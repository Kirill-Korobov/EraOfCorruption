using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CloseCombatEnemy : MonoBehaviour
{
    [SerializeField] private EnemiesInfo enemiesInfo;
    [SerializeField] private QuestTaskVariableValuesInfo questTaskVariableValuesInfo;
    [SerializeField] private int enemyIndex;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private Image _HPBarImage;
    [SerializeField] private TMP_Text _HPBarText;
    [SerializeField] private AnimationClip dieAnimationClip;
    private SpawnEnemies spawnEnemies;
    private ForestDragon forestDragon;
    private PauseManager pauseManager;
    private MC_HealthManager healthManager;
    private MC_LevelManager levelManager;
    private Transform mainCharacterTransform;
    private NavMeshAgent enemyAgent;
    private Animator animator;
    private Coroutine dieCoroutine;
    private float currentHP, currentAttackRechargeTime, currentWanderTime, timeInAttackRange, timeOutOfAttackRange, attackEnterDelay, attackExitDelay;
    private bool mainCharacterIsInAttackRadius, isWandering, isChasing, isAttacking;
    [HideInInspector] public bool spawnedBySpawnEnemies;
    [HideInInspector] public bool isDead;
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
                if (dieCoroutine == null)
                {
                    dieCoroutine = StartCoroutine(Die());
                }
            }
            else if (value > enemiesInfo.enemiesInfo[enemyIndex].HP)
            {
                currentHP = enemiesInfo.enemiesInfo[enemyIndex].HP;
            }
            else
            {
                currentHP = value;
            }
        }
    }

    private void Awake()
    {
        isDead = false;
        enemyNameText.text = enemiesInfo.enemiesInfo[enemyIndex].Name;
        CurrentHP = enemiesInfo.enemiesInfo[enemyIndex].HP;
        mainCharacterTransform = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        isWandering = false;
        isChasing = false;
        enemyAgent.angularSpeed = float.MaxValue;
        enemyAgent.autoBraking = false;
        enemyAgent.updateRotation = true;
        enemyAgent.acceleration = float.MaxValue;
        enemyAgent.stoppingDistance = 0f;
        timeInAttackRange = 0f;
        timeOutOfAttackRange = 0f;
        attackEnterDelay = 0.1f;
        attackExitDelay = 0.1f;
        pauseManager = FindAnyObjectByType<PauseManager>();
        spawnEnemies = FindAnyObjectByType<SpawnEnemies>();
        forestDragon = FindAnyObjectByType<ForestDragon>();
        questTasks = FindAnyObjectByType<QuestTasks>();
        healthManager = mainCharacterTransform.gameObject.GetComponent<MC_HealthManager>();
        levelManager = mainCharacterTransform.gameObject.GetComponent<MC_LevelManager>();
        animator = GetComponent<Animator>();
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
        // For Test

        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(10000);
        }

        //

        if (!pauseManager.pause && !isDead)
        {
            if (Vector3.Distance(gameObject.transform.position, mainCharacterTransform.position) >= enemiesInfo.EnemyDisappearRadius)
            {
                Dissapear();
            }
            _HPBarImage.fillAmount = CurrentHP / enemiesInfo.enemiesInfo[enemyIndex].HP;
            _HPBarText.text = $"{(int)CurrentHP} / {(int)enemiesInfo.enemiesInfo[enemyIndex].HP}";
            mainCharacterIsInAttackRadius = Vector3.Distance(gameObject.transform.position, mainCharacterTransform.position) <= enemiesInfo.enemiesInfo[enemyIndex].AttackRadius;
            Move();
            if (mainCharacterIsInAttackRadius && !isAttacking)
            {
                timeInAttackRange += Time.deltaTime;
                timeOutOfAttackRange = 0f;

                if (!isAttacking && timeInAttackRange >= attackEnterDelay)
                {
                    isChasing = false;
                    isWandering = false;
                    animator.Play("Attack");
                    isAttacking = true;
                }
            }
            else
            {
                timeOutOfAttackRange += Time.deltaTime;
                timeInAttackRange = 0f;

                if (isAttacking && timeOutOfAttackRange >= attackExitDelay)
                {
                    isAttacking = false;
                }
            }
            if (mainCharacterIsInAttackRadius && currentAttackRechargeTime <= 0f)
            {
                Attack();
                currentAttackRechargeTime = enemiesInfo.enemiesInfo[enemyIndex].RechargeTime;
            }
            if (currentAttackRechargeTime > 0f)
            {
                currentAttackRechargeTime -= Time.deltaTime;
            }
        }
    }

    private void Move()
    {
        if (enemyAgent.isOnNavMesh)
        {
            if (mainCharacterIsInAttackRadius)
            {
                transform.LookAt(new Vector3(mainCharacterTransform.position.x, transform.position.y, mainCharacterTransform.position.z));
            }
            else if (Vector3.Distance(gameObject.transform.position, mainCharacterTransform.position) <= enemiesInfo.enemiesInfo[enemyIndex].VisionRadius)
            {
                if (Vector3.Distance(gameObject.transform.position, mainCharacterTransform.position) >= enemiesInfo.enemiesInfo[enemyIndex].AttackRadius)
                {
                    Vector3 destination = mainCharacterTransform.position - (mainCharacterTransform.position - gameObject.transform.position).normalized * 0.9f * enemiesInfo.enemiesInfo[enemyIndex].AttackRadius;
                    enemyAgent.SetDestination(destination);
                    if (!isChasing)
                    {
                        isAttacking = false;
                        isWandering = false;
                        enemyAgent.speed = enemiesInfo.enemiesInfo[enemyIndex].ChaseSpeed;
                        animator.Play("Chase");
                        isChasing = true;
                    }
                }
            }
            else
            {
                if (currentWanderTime <= 0f || enemyAgent.remainingDistance < 0.5f)
                {
                    enemyAgent.SetDestination(GetRandomNavMeshLocation());
                    currentWanderTime = enemiesInfo.WanderDestinationRefreshTime;
                }
                else
                {
                    currentWanderTime -= Time.deltaTime;
                }
                if (!isWandering)
                {
                    isAttacking = false;
                    isChasing = false;
                    enemyAgent.speed = enemiesInfo.enemiesInfo[enemyIndex].WanderSpeed;
                    animator.Play("Wander");
                    isWandering = true;
                }
            }
        }
    }

    private Vector3 GetRandomNavMeshLocation()
    {
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * enemiesInfo.WanderDestinatioMaxRadius, out NavMeshHit hit, enemiesInfo.WanderDestinatioMaxRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    private void Attack()
    {
        healthManager.TakeDamage(enemiesInfo.enemiesInfo[enemyIndex].Damage);
    }

    public void TakeDamage(float value)
    {
        if (enemiesInfo.enemiesInfo[enemyIndex].Defense >= value)
        {
            CurrentHP -= 1;
        }
        else
        {
            CurrentHP -= value - enemiesInfo.enemiesInfo[enemyIndex].Defense;
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        if (spawnedBySpawnEnemies)
        {
            spawnEnemies.enemyCounter--;
        }
        else if (forestDragon != null)
        {
            forestDragon.spawnedEnemiesCounter--;
        }
        enemyAgent.enabled = false;
        GetComponentInChildren<NicknameCanvasBehaviour>().gameObject.SetActive(false);
        levelManager.IncreaseXP(enemiesInfo.enemiesInfo[enemyIndex].XPDropAmount);
        // get money
        // drop items
        if (gameObject.TryGetComponent<BanditMarker>(out var banditMarker) && currentGameStats.questStagesStats.questStages[questTaskVariableValuesInfo.BanditKillerIndex] == QuestStages.inProgress)
        {
            currentGameStats.questVariableStats.banditKillerKilledBanditNumber++;
            questTasks.UpdateTasks();
        }
        if (gameObject.TryGetComponent<MushroomMarker>(out var mushroomMarker) && currentGameStats.questStagesStats.questStages[questTaskVariableValuesInfo.SporeWarIndex] == QuestStages.inProgress)
        {
            currentGameStats.questVariableStats.sporeWarKilledMushroomNumber++;
            questTasks.UpdateTasks();
        }
        if (gameObject.TryGetComponent<GoblinMarker>(out var goblinMarker) && currentGameStats.questStagesStats.questStages[questTaskVariableValuesInfo.GoblinTroubleIndex] == QuestStages.inProgress)
        {
            currentGameStats.questVariableStats.goblinTroubleKilledGoblinNumber++;
            questTasks.UpdateTasks();
        }
        animator.Play("Die");
        yield return new WaitForSeconds(dieAnimationClip.length + 1);
        Destroy(gameObject);
        dieCoroutine = null;
    }

    public void Dissapear()
    {
        if (spawnedBySpawnEnemies)
        {
            spawnEnemies.enemyCounter--;
        }
        else if (forestDragon != null)
        {
            forestDragon.spawnedEnemiesCounter--;
        }
        Destroy(gameObject);
    }
}