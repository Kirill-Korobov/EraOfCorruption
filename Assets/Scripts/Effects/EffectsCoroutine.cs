using System;
using System.Collections;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

public class EffectsCoroutine : MonoBehaviour
{
    [SerializeField] private StatEffects statEffects;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_MovementManager movementManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private Image[] effectsImage;
    [SerializeField] private TMP_Text[] effectsTexts;
    [SerializeField] private Camera[] mainCamera = new Camera[3];

    private Coroutine[] effectsCoroutine = new Coroutine[16];
    private Coroutine[] effectsCoroutineTimer = new Coroutine[16];

    private Action[] resets;
    private float speed;
    private float speedWalk;
    private float[] blindess = new float[3];
    private int defense;



    private void Awake()
    {

        speed = movementManager.Speed;
        //float speedWalk = movementManager.walkSpeed;
        resets = new Action[16];
        resets[0] = PoisonStop;
        resets[1] = WeaknessStop;
        resets[2] = SlownessStop;
        StaticEffects.weaknessRate = 1;
        StaticEffects.strengthRate = 1;
        StaticEffects.hunger = 1;
        StaticEffects.vampirismHPRate = statEffects.VampirismHP;
        StaticEffects.vampirismHP = false;
        StaticEffects.shock = false;
        StaticEffects.coroutines = this;

    }

    private IEnumerator ShowATimer(int i, int timer)
    {
        int minutes = timer / 60;
        int seconds = timer % 60;
        while (true)
        {
            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    break;
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }
            else
            {
                seconds--;
            }
            if (seconds >= 10)
            {
                effectsTexts[i].text = $"{minutes}:{seconds}";
            }
            else
            {
                effectsTexts[i].text = $"{minutes}:0{seconds}";
            }
            yield return new WaitForSeconds(1);
        }
        if (i == 0 || i == 5 || i == 9)
        {
            StopCoroutine(effectsCoroutine[i]);
        }
        
        resets[i]();
        EffectsPosition.DeleteImageAsync(effectsImage[i]);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Poison();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Poison();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Blindness();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Slowness();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Resistance();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Shocks();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Strength();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Cursed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Hex();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Regeneration();
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
        }
    }

    public void StartEffectCoroutine(Func<IEnumerator> coroutineMethod, int i, int timer)
    {
        if (effectsCoroutine[i] == null)
        {
            effectsCoroutine[i] = StartCoroutine(coroutineMethod());
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            EffectsPosition.SetImageAsync(effectsImage[i]);
            return;
        }
        
        else
        {
            StopCoroutine(effectsCoroutineTimer[i]);
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            return;
        }
    }
    public void StartEffectCoroutine(Action coroutineMethod, int i, int timer)
    {
        if (effectsCoroutine[i] == null)
        {
            coroutineMethod();
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            EffectsPosition.SetImageAsync(effectsImage[i]);
            return;
        }

        else
        {
            StopCoroutine(effectsCoroutineTimer[i]);
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            return;
        }
    }
    public void Poison() => StartEffectCoroutine(() => PoisonCoroutine(), 0, statEffects.PoisionTime);
    private IEnumerator PoisonCoroutine()
    {
        
        
        float timer = 0;
        WaitForSeconds a = new WaitForSeconds(1);
        int n = 0;
        effectsImage[0].gameObject.SetActive(true);
        while (true)
        {
            //healthManager.CurrentHealth = healthManager.CurrentHealth - (statEffects.PoisionDMG + statEffects.PoisionDIS * healthManager.CurrentHealth / 100);
            n++;
            while (statEffects.PoisionCD * n > timer)
            {
                timer++;
                yield return a;
            }
        }
    }
    private void PoisonStop()
    {
        effectsImage[0].gameObject.SetActive(true);
    }
    public void Weakness() => StartEffectCoroutine(() => WeaknessCoroutine(), 1, statEffects.WeaknessTime);

    private void WeaknessCoroutine()
    {
        effectsImage[1].gameObject.SetActive(true);
        StaticEffects.weaknessRate = statEffects.WeaknessRate;
    }
    private void WeaknessStop()
    {
        effectsImage[1].gameObject.SetActive(false);
        StaticEffects.weaknessRate = 1;
    }
    public void Slowness() => StartEffectCoroutine(() => SlownessCoroutine(), 2, statEffects.SlownessTime);


    private void SlownessCoroutine()
    {
        effectsImage[2].gameObject.SetActive(true);
        movementManager.Speed = statEffects.SlownessRate * movementManager.Speed / 100;
        //movementManager.walkSpeed = statEffects.SlownessRate * movementManager.walkSpeed / 100;
    }

    private void SlownessStop()
    {
        movementManager.Speed = speed;
        //movementManager.walkSpeed = speedWalk;
        effectsImage[2].gameObject.SetActive(false);
    }
    public void Hunger() => StartEffectCoroutine(() => HungerCoroutine(), 3, statEffects.HungerTime);
    private void HungerCoroutine()
    {
        effectsImage[3].gameObject.SetActive(true);
        StaticEffects.hunger = statEffects.HungerRate/100;
    }
    private void HungerStop()
    {
        StaticEffects.hunger = 1;
        effectsImage[3].gameObject.SetActive(false);
    }
    public void Partialpenetration() => StartEffectCoroutine(() => PartialpenetrationCoroutine(), 4, statEffects.PenetrationTime);
    private void PartialpenetrationCoroutine()
    {
        //healthManager.penetration = statEffects.PenetrationRate / 100;
        effectsImage[4].gameObject.SetActive(true);
    }
    private void PartialpenetrationStop()
    {
        //healthManager.penetration = 1;
        effectsImage[4].gameObject.SetActive(false);
    }
    public void Burn() => StartEffectCoroutine(() => BurnCoroutine(), 5, statEffects.BurnTime);
    private IEnumerator BurnCoroutine()
    {
        /*int i;
        if (healthManager.MaxHealth < 250)
        {
            i = 0;
        }
        else if (healthManager.MaxHealth < 500)
        {
            i = 1;
        }
        else
        {
            i = 2;
        }*/
        float timer = statEffects.BurnCD;
        int n = 0;
        effectsImage[5].gameObject.SetActive(true);
        WaitForSeconds a = new WaitForSeconds(statEffects.BurnCD);
        while (true)
        {
            //healthManager.CurrentHealth -= statEffects.BurnDMG[i];
            n++;
            while (statEffects.PoisionCD * n > timer)
            {
                timer++;
                yield return a;
            }
        }
    }
    private void BurnStop()
    {
        effectsImage[5].gameObject.SetActive(false);
    }
    public void Blindness() => StartEffectCoroutine(() => BlindnessCoroutine(), 6, statEffects.BlindnessTime);
    private void BlindnessCoroutine()
    {
        effectsImage[6].gameObject.SetActive(true);
        blindess[0] = mainCamera[0].farClipPlane;
        blindess[1] = mainCamera[1].farClipPlane;
        blindess[2] = mainCamera[2].farClipPlane;
        for (int i = 0; i < 3; i++)
        {
            mainCamera[i].farClipPlane = statEffects.BlindnessRate;
        }
    }
    private void BlidnessStop()
    {
        mainCamera[0].farClipPlane = blindess[0];
        mainCamera[1].farClipPlane = blindess[1];
        mainCamera[2].farClipPlane = blindess[2];
        effectsImage[6].gameObject.SetActive(false);
    }
    public void Cursed() => StartEffectCoroutine(() => CursedCoroutine(), 7, statEffects.CursedTime);
    private void CursedCoroutine()
    {
        healthManager.cursed = true;
        effectsImage[7].gameObject.SetActive(true);
    }
    private void CursedStop()
    {
        healthManager.cursed = false;
        effectsImage[7].gameObject.SetActive(false);
    }
    public void Hex() => StartEffectCoroutine(() => HexCoroutine(), 8, statEffects.HexTime);
    private void HexCoroutine()
    {
        healthManager.hex = statEffects.HexRate;
        effectsImage[8].gameObject.SetActive(true);
    }
    private void HexStop()
    {
        healthManager.hex = 1;
        effectsImage[8].gameObject.SetActive(false);
    }
    public void Regeneration() => StartEffectCoroutine(() => RegenerationCoroutine(), 9, statEffects.RegenerationTime);
    private IEnumerator RegenerationCoroutine()
    {
        WaitForSeconds a = new WaitForSeconds(statEffects.RegenerationCD);
        float timer = 0;
        int n = 0;
        effectsImage[9].gameObject.SetActive(true);
        while (true)
        {
            healthManager.GetHealth(statEffects.Regeneration);
            n++;
            while (statEffects.RegenerationCD * n > timer)
            {
                timer++;
                yield return a;
            }
        }
    }
    private void RegenerationStop()
    {
        effectsImage[9].gameObject.SetActive(false);
    }
    public void Strength() => StartEffectCoroutine(() => StrengthCoroutine(), 10, statEffects.StrengthTime);

    private void StrengthCoroutine()
    {
        effectsImage[10].gameObject.SetActive(true);
        StaticEffects.strengthRate = statEffects.StrengthRate;
    }
    private void StrengthStop()
    {
        effectsImage[10].gameObject.SetActive(false);
        StaticEffects.strengthRate = statEffects.StrengthRate;
    }
    public void Speed() => StartEffectCoroutine(() => SpeedCoroutine(), 11, statEffects.SpeedTime);


    private void SpeedCoroutine()
    {
        effectsImage[11].gameObject.SetActive(true);
        // PEREROBUTU
    }
    private void SpeedStop()
    {
        effectsImage[11].gameObject.SetActive(false);
    }

    public void VampirismHP() => StartEffectCoroutine(() => VampirisimHPCoroutine(), 12, statEffects.VampirismHPTime);

    private void VampirisimHPCoroutine()
    {
        StaticEffects.vampirismHP = true;
        effectsImage[12].gameObject.SetActive(true);
    }
    private void VampirismHPStop()
    {
        effectsImage[12].gameObject.SetActive(false);
        StaticEffects.vampirismHP = false;
    }

    public void VampirismHPLogic(int value)
    {
        healthManager.GetHealth(value * statEffects.VampirismHP / 100);
    }
    public void VampirismMana() => StartEffectCoroutine(() => VampirisimManaCoroutine(), 13, statEffects.VampirismManaTime);

    private void VampirisimManaCoroutine()
    {
        StaticEffects.vampirismMana = true;
        effectsImage[13].gameObject.SetActive(true);
    }
    private void VampirismManaStop()
    {
        effectsImage[13].gameObject.SetActive(false);
        StaticEffects.vampirismMana = false;
    }

    public void VampirismManaLogic(int value)
    {
        manaManager.ReplenishMana(value * statEffects.VampirismMana / 100);
    }
    public void Resistance() => StartEffectCoroutine(() => ResistanceCoroutine(), 14, statEffects.ResistanceTime);

    private void ResistanceCoroutine()
    {
        effectsImage[14].gameObject.SetActive(true);
        defense = healthManager.Defense;
        healthManager.Defense = (int)(statEffects.ResistanceRate * healthManager.Defense / 100);
    }
    private void ResistanceStop()
    {
        healthManager.Defense = defense;
        effectsImage[14].gameObject.SetActive(false);
    }
    public void Shocks() => StartEffectCoroutine(() => ShocksCoroutine(), 15, statEffects.ShocksTime);

    private void ShocksCoroutine()
    {
        effectsImage[15].gameObject.SetActive(true);
        StaticEffects.shock = true;
    }
    private void ShocksStop()
    {
        StaticEffects.shock = false;
        effectsImage[15].gameObject.SetActive(false);
    }

}
