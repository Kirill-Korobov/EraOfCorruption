using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ForestDragon : MonoBehaviour
{
    [SerializeField] private ForestDragonInfo forestDragonInfo;
    [SerializeField] private EnemiesInfo enemiesInfo;
    [SerializeField] private AnimationClip appearAnimationClip, takeOffAnimationClip, landAnimationClip, coverAnimationClip, uncoverAnimationClip, getHitAnimationClip, dieAnimationClip;
    private Transform mainCharacterTransform, enemiesParentTransform;
    private NavMeshAgent agent;
    private Animator animator;
    private SpawnForestDragon spawnForestDragon;
    private BossUICanvasOperator bossUICanvasOperator;
    private MC_HealthManager healthManager;
    private MC_LevelManager levelManager;
    private PauseManager pauseManager;
    private Coroutine appearCoroutine, changeAttackCoroutine, dieCoroutine;
    private ForestDragonAttack currentForestDragonAttack;
    private float currentHP, changeAttackBufferTime, changeAttackTimeAmount, biteBufferRechargeTime, spawnEnemiesBufferRechargeTime, bufferDashTimer;
    [HideInInspector] public float currentPhaseStatsMultiplier;
    private int currentDefense;
    [HideInInspector] public int spawnedEnemiesCounter;
    private bool isDead, decorativeAnimationIsPlaying, phaseHasChanged, isDashing;
    [SerializeField] private QuestTaskVariableValuesInfo questTaskVariableValuesInfo;
    private QuestTasks questTasks;
    private GameStatsManager gameStatsManager;
    private GameStats currentGameStats;

    public float CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            if (value <= 0)
            {
                currentHP = 0;
                if (dieCoroutine == null)
                {
                    dieCoroutine = StartCoroutine(Die());
                }
            }
            else if (value > forestDragonInfo.TotalHP)
            {
                currentHP = forestDragonInfo.TotalHP;
            }
            else
            {
                currentHP = value;
            }
            bossUICanvasOperator.UpdateHPUI(currentHP);
            if (!phaseHasChanged && currentHP <= forestDragonInfo.TotalHP * forestDragonInfo.ChangePhaseHPPersent)
            {
                changeAttackBufferTime = 0f; 
            }
        }
    }

    private void Awake()
    {
        isDead = false;
        phaseHasChanged = false;
        currentPhaseStatsMultiplier = forestDragonInfo.FirstPhaseStatsMultiplier;
        bossUICanvasOperator = FindAnyObjectByType<BossUICanvasOperator>();
        bossUICanvasOperator.SetUIActive(forestDragonInfo.Name, forestDragonInfo.TotalHP, forestDragonInfo.ChangePhaseHPPersent);
        CurrentHP = forestDragonInfo.TotalHP;
        currentDefense = forestDragonInfo.GeneralDefense;
        spawnForestDragon = FindAnyObjectByType<SpawnForestDragon>();   
        transform.position = forestDragonInfo.SpawnPosition;
        pauseManager = FindAnyObjectByType<PauseManager>();
        questTasks = FindAnyObjectByType<QuestTasks>();
        mainCharacterTransform = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        enemiesParentTransform = spawnForestDragon.gameObject.transform;
        transform.LookAt(new Vector3(mainCharacterTransform.position.x, transform.position.y, mainCharacterTransform.position.z));
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0f;
        agent.angularSpeed = float.MaxValue;
        agent.acceleration = float.MaxValue;
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.stoppingDistance = 0f;
        agent.enabled = false;
        healthManager = mainCharacterTransform.gameObject.GetComponent<MC_HealthManager>();
        levelManager = mainCharacterTransform.gameObject.GetComponent<MC_LevelManager>();
        changeAttackTimeAmount = forestDragonInfo.AverageAttackDuration * UnityEngine.Random.Range(forestDragonInfo.MinAttackDurationMultiplier, forestDragonInfo.MaxAttackDurationMultiplier);
        changeAttackBufferTime = changeAttackTimeAmount;
        spawnEnemiesBufferRechargeTime = forestDragonInfo.SpawnEnemiesRechargeTime / currentPhaseStatsMultiplier;
        spawnedEnemiesCounter = 0;
        decorativeAnimationIsPlaying = false;
        animator = GetComponent<Animator>();
        if (appearCoroutine == null)
        {
            appearCoroutine = StartCoroutine(Appear());
        }
    }

    private void Start()
    {
        gameStatsManager = FindAnyObjectByType<GameStatsManager>();
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
    }

    private void Update()
    {
        if (!pauseManager.pause && !isDead)
        {
            if (!decorativeAnimationIsPlaying)
            {
                if (changeAttackBufferTime <= 0f)
                {
                    if (changeAttackCoroutine == null)
                    {
                        changeAttackCoroutine = StartCoroutine(ChangeAttack());
                    }
                }
                else
                {
                    changeAttackBufferTime -= Time.deltaTime;
                    switch (currentForestDragonAttack)
                    {
                        case ForestDragonAttack.bite:
                            transform.LookAt(new Vector3(mainCharacterTransform.position.x, transform.position.y, mainCharacterTransform.position.z));
                            if (Vector3.Distance(transform.position, mainCharacterTransform.position) <= forestDragonInfo.BiteRadius)
                            {
                                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Bite"))
                                {
                                    animator.Play("Bite");
                                }
                                else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bite"))
                                {
                                    animator.SetTrigger("WalkToBite");
                                }
                                if (biteBufferRechargeTime <= 0f)
                                {
                                    healthManager.TakeDamage(forestDragonInfo.BiteDamage);
                                    biteBufferRechargeTime = forestDragonInfo.BiteRechargeTime;
                                }
                            }
                            else
                            {
                                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Bite"))
                                {
                                    animator.Play("Walk");
                                }
                                else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                                {
                                    animator.SetTrigger("BiteToWalk");
                                }
                                agent.SetDestination(mainCharacterTransform.position - (mainCharacterTransform.position - gameObject.transform.position).normalized * 0.9f * forestDragonInfo.BiteRadius);
                            }
                            biteBufferRechargeTime -= Time.deltaTime;
                            break;
                        case ForestDragonAttack.shootFireball:
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("FlyShootFireball"))
                            {
                                animator.Play("FlyShootFireball");
                            }
                            transform.LookAt(new Vector3(mainCharacterTransform.position.x, transform.position.y, mainCharacterTransform.position.z));
                            break;
                        case ForestDragonAttack.spawnEnemies:
                            if (spawnEnemiesBufferRechargeTime <= 0f && spawnedEnemiesCounter < forestDragonInfo.MaxSpawnedEnemiesNumber)
                            {
                                int chooseEnemy = UnityEngine.Random.Range(0, 2);
                                int bufferIndex;
                                if (chooseEnemy == 0)
                                {
                                    bufferIndex = forestDragonInfo.OrcIndex;
                                }
                                else
                                {
                                    bufferIndex = forestDragonInfo.GolemIndex;
                                }
                                GameObject bufferEnemy = Instantiate(enemiesInfo.enemiesInfo[bufferIndex].EnemyPrefab, Vector3.zero, Quaternion.identity.normalized, transform);
                                bufferEnemy.GetComponent<CloseCombatEnemy>().spawnedBySpawnEnemies = false;
                                NavMeshAgent bufferEnemyAgent = bufferEnemy.GetComponent<NavMeshAgent>();
                                bufferEnemyAgent.enabled = false;
                                bufferEnemy.transform.localPosition = forestDragonInfo.EnemySpawnPosition;
                                bufferEnemy.transform.parent = enemiesParentTransform;
                                bufferEnemyAgent.Warp(bufferEnemy.transform.position);
                                bufferEnemyAgent.enabled = true;
                                spawnedEnemiesCounter++;
                                spawnEnemiesBufferRechargeTime = forestDragonInfo.SpawnEnemiesRechargeTime / currentPhaseStatsMultiplier;
                            }
                            else
                            {
                                spawnEnemiesBufferRechargeTime -= Time.deltaTime;
                            }
                            break;
                        case ForestDragonAttack.heal:
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("FlyFloat"))
                            {
                                animator.Play("FlyFloat");
                                currentDefense = Mathf.CeilToInt(forestDragonInfo.GeneralDefense * forestDragonInfo.HealDefenseMultiplier);
                            }
                            Heal(forestDragonInfo.HealSpeed * Time.deltaTime);
                            break;
                        case ForestDragonAttack.dash:
                            if (isDashing)
                            {
                                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                                {
                                    animator.Play("Dash");
                                    currentDefense = Mathf.CeilToInt(forestDragonInfo.GeneralDefense * forestDragonInfo.DashDefenseMultiplier);
                                    agent.isStopped = false;
                                }
                                agent.SetDestination(mainCharacterTransform.position);
                                transform.LookAt(new Vector3(mainCharacterTransform.position.x, transform.position.y, mainCharacterTransform.position.z));
                                bufferDashTimer -= Time.deltaTime;
                                if (bufferDashTimer <= 0)
                                {
                                    isDashing = false;
                                    bufferDashTimer = forestDragonInfo.DashStopTime;
                                }
                            }
                            else
                            {
                                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DashStop"))
                                {
                                    animator.Play("DashStop");
                                    currentDefense = Mathf.CeilToInt(forestDragonInfo.GeneralDefense * forestDragonInfo.DashStopDefenseMultiplier);
                                    agent.isStopped = true;
                                }
                                bufferDashTimer -= Time.deltaTime;
                                if (bufferDashTimer <= 0)
                                {
                                    isDashing = true;
                                    bufferDashTimer = forestDragonInfo.DashTime;
                                }
                            }
                            break;
                    }
                }
            }
                
            // For Test

            if (Input.GetKeyDown(KeyCode.U))
            {
                TakeDamage(20);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                TakeDamage(1000);
            }

            //
        }
    }

    private IEnumerator ChangeAttack()
    {
        animator.Play("Idle");
        if (currentForestDragonAttack == ForestDragonAttack.bite || currentForestDragonAttack == ForestDragonAttack.dash)
        {
            agent.enabled = false;
            agent.speed = 0f;
        }
        else if (currentForestDragonAttack == ForestDragonAttack.shootFireball || currentForestDragonAttack == ForestDragonAttack.heal)
        {
            decorativeAnimationIsPlaying = true;
            animator.Play("Land");
            yield return new WaitForSeconds(landAnimationClip.length);
            decorativeAnimationIsPlaying = false;
        }
        else if (currentForestDragonAttack == ForestDragonAttack.spawnEnemies)
        {
            decorativeAnimationIsPlaying = true;
            animator.Play("Uncover");
            yield return new WaitForSeconds(uncoverAnimationClip.length);
            decorativeAnimationIsPlaying = false;
        }
        currentDefense = forestDragonInfo.GeneralDefense;
        if (!phaseHasChanged && currentHP <= forestDragonInfo.TotalHP * forestDragonInfo.ChangePhaseHPPersent)
        {
            animator.Play("GetHit");
            decorativeAnimationIsPlaying = true;
            yield return new WaitForSeconds(getHitAnimationClip.length);
            decorativeAnimationIsPlaying = false;
            phaseHasChanged = true;
            currentPhaseStatsMultiplier = forestDragonInfo.SecondPhaseStatsMultiplier;
        }
        currentForestDragonAttack = ChooseAttack();
        if (currentForestDragonAttack == ForestDragonAttack.bite)
        {
            agent.enabled = true;
            agent.speed = forestDragonInfo.WalkSpeed;
        }
        else if (currentForestDragonAttack == ForestDragonAttack.dash)
        {
            agent.enabled = true;
            agent.speed = forestDragonInfo.DashSpeed;
            agent.SetDestination(mainCharacterTransform.position);
            isDashing = true;
            bufferDashTimer = forestDragonInfo.DashTime;
        }
        else if (currentForestDragonAttack == ForestDragonAttack.shootFireball || currentForestDragonAttack == ForestDragonAttack.heal)
        {
            decorativeAnimationIsPlaying = true;
            animator.Play("TakeOff");
            yield return new WaitForSeconds(takeOffAnimationClip.length);
            decorativeAnimationIsPlaying = false;
        }
        else if (currentForestDragonAttack == ForestDragonAttack.spawnEnemies)
        {
            decorativeAnimationIsPlaying = true;
            animator.Play("Cover");
            yield return new WaitForSeconds(coverAnimationClip.length);
            spawnEnemiesBufferRechargeTime = forestDragonInfo.SpawnEnemiesRechargeTime / currentPhaseStatsMultiplier;
            currentDefense = Mathf.CeilToInt(forestDragonInfo.GeneralDefense * forestDragonInfo.SpawnEnemiesDefenseMultiplier);
            decorativeAnimationIsPlaying = false;
        }
        if (!phaseHasChanged && currentHP <= forestDragonInfo.TotalHP * forestDragonInfo.ChangePhaseHPPersent)
        {
            changeAttackCoroutine = StartCoroutine(ChangeAttack());
        }
        else
        {
            changeAttackTimeAmount = forestDragonInfo.AverageAttackDuration * UnityEngine.Random.Range(forestDragonInfo.MinAttackDurationMultiplier, forestDragonInfo.MaxAttackDurationMultiplier);
            changeAttackBufferTime = changeAttackTimeAmount;
            changeAttackCoroutine = null;
        } 
    }

    private ForestDragonAttack ChooseAttack()
    {
        var forestDragonAttacks = (ForestDragonAttack[])Enum.GetValues(typeof(ForestDragonAttack));
        ForestDragonAttack bufferForestDragonAttack;
        while (true)
        { 
            bufferForestDragonAttack = forestDragonAttacks[UnityEngine.Random.Range(0, forestDragonAttacks.Length)];
            if (bufferForestDragonAttack != currentForestDragonAttack)
            {
                if (!phaseHasChanged && bufferForestDragonAttack != ForestDragonAttack.heal && bufferForestDragonAttack != ForestDragonAttack.dash || phaseHasChanged && bufferForestDragonAttack != ForestDragonAttack.bite)
                {
                    break;
                }
            }
        }
        
        return bufferForestDragonAttack;
    }

    private IEnumerator Appear()
    {
        animator.Play("Appear");
        decorativeAnimationIsPlaying = true;
        yield return new WaitForSeconds(appearAnimationClip.length);
        decorativeAnimationIsPlaying = false;
        if (changeAttackCoroutine == null)
        {
            changeAttackCoroutine = StartCoroutine(ChangeAttack());
        }
        appearCoroutine = null;
    }

    private void ShootFireball()
    {
        GameObject fireBall = Instantiate(forestDragonInfo.FireballPrefab, forestDragonInfo.FireballSpawnPosition, Quaternion.identity, transform);
        fireBall.transform.localPosition = forestDragonInfo.FireballSpawnPosition;
        fireBall.transform.parent = enemiesParentTransform;
        fireBall.GetComponent<ForestDragonFireball>().SetFlightDirection((mainCharacterTransform.position - fireBall.transform.position).normalized);
    }

    public void TakeDamage(float value)
    {
        if (currentDefense >= value)
        {
            CurrentHP -= 1;
        }
        else
        {
            CurrentHP -= value - currentDefense;
        }
    }

    private void Heal(float value)
    {
        CurrentHP += value;
    }

    private IEnumerator Die()
    {
        isDead = true;   
        if (appearCoroutine != null)
        {
            StopCoroutine(appearCoroutine);
            appearCoroutine = null;
        }
        if (changeAttackCoroutine != null)
        {
            StopCoroutine(changeAttackCoroutine);
            changeAttackCoroutine = null;
        }
        agent.enabled = false;
        levelManager.IncreaseXP(forestDragonInfo.XPDropAmount);
        // get money
        // drop items
        if (currentGameStats.questStagesStats.questStages[questTaskVariableValuesInfo.HeroOfTheForestIndex] == QuestStages.inProgress && !currentGameStats.questVariableStats.killedForestDragon)
        {
            currentGameStats.questVariableStats.killedForestDragon = true;
            questTasks.UpdateTasks();
        }
        animator.Play("Die");
        yield return new WaitForSeconds(dieAnimationClip.length + 1);
        spawnForestDragon.bossIsSpawned = false;
        bossUICanvasOperator.SetUIInactive();
        Destroy(gameObject);
        bossUICanvasOperator.ShowCongratsWindow();
        dieCoroutine = null;
    }

    public void Dissapear()
    {
        spawnForestDragon.bossIsSpawned = false;
        bossUICanvasOperator.SetUIInactive();
        Destroy(gameObject);
    }
}