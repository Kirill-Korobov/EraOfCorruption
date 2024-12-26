using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectsCoroutine : MonoBehaviour
{
    [SerializeField] private StatEffects statEffects;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_MovementManager movementManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private Image[] effectsImage;
    [SerializeField] private Camera[] mainCamera = new Camera[3];

    private Coroutine[] effectsCoroutine = new Coroutine[16];

    private void Awake()
    {
        StaticEffects.weaknessRate = 1;
        StaticEffects.strengthRate = 1;
        StaticEffects.vampirismHPRate = statEffects.VampirismHP;
        StaticEffects.vampirismHP = false;
        StaticEffects.shock = false;
        StaticEffects.coroutines = this;

    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Poison();
        }
    }

    public void StartEffectCoroutine(Func<IEnumerator> coroutineMethod, int i)
    {
        if (effectsCoroutine[i] == null)
        {
            effectsCoroutine[i] = StartCoroutine(coroutineMethod());
            return;
        }
        else
        {
            StopCoroutine(effectsCoroutine[i]);
            effectsCoroutine[i] = StartCoroutine(coroutineMethod());
            return;
        }
    }
    public void Poison() => StartEffectCoroutine(() => PoisonCoroutine(), 0);
    private IEnumerator PoisonCoroutine()
    {
        WaitForSeconds timer = new WaitForSeconds(statEffects.PoisionCD);
        int n = 0;
        effectsImage[0].gameObject.SetActive(true);
        while (statEffects.PoisionCD * n < statEffects.PoisionTime)
        {
            healthManager.CurrentHealth = healthManager.CurrentHealth - (statEffects.PoisionDMG + statEffects.PoisionDIS * healthManager.CurrentHealth / 100);
            Debug.Log(healthManager.CurrentHealth);
            yield return timer;
            n++;
        }
        healthManager.CurrentHealth = healthManager.CurrentHealth - (statEffects.PoisionDMG + statEffects.PoisionDIS * healthManager.CurrentHealth / 100);
        Debug.Log(healthManager.CurrentHealth);
        effectsImage[0].gameObject.SetActive(false);
    }
    public void Weakness() => StartEffectCoroutine(() => WeaknessCoroutine(), 1);

    private IEnumerator WeaknessCoroutine()
    {
        effectsImage[1].gameObject.SetActive(true);
        StaticEffects.weaknessRate = statEffects.WeaknessRate;
        yield return new WaitForSeconds(statEffects.WeaknessTime);
        effectsImage[1].gameObject.SetActive(false);
        StaticEffects.weaknessRate = statEffects.WeaknessRate;
    }
    public void Slowness() => StartEffectCoroutine(() => SlownessCoroutine(), 2);


    private IEnumerator SlownessCoroutine()
    {
        effectsImage[2].gameObject.SetActive(true);
        float speed = movementManager.Speed;
        movementManager.Speed = statEffects.SlownessRate * movementManager.Speed / 100;
        yield return new WaitForSeconds(statEffects.SlownessTime);
        movementManager.Speed = speed;
        effectsImage[2].gameObject.SetActive(false);
    }
    public void Hunger() => StartEffectCoroutine(() => HungerCoroutine(), 3);
    private IEnumerator HungerCoroutine()
    {
        effectsImage[3].gameObject.SetActive(true);
        float speed = movementManager.Speed;
        movementManager.Speed = statEffects.SlownessRate * movementManager.Speed / 100;
        yield return new WaitForSeconds(statEffects.SlownessTime);
        movementManager.Speed = speed;
        effectsImage[3].gameObject.SetActive(false);
    }
    public void Partialpenetration() => StartEffectCoroutine(() => PartialpenetrationCoroutine(), 4);
    private IEnumerator PartialpenetrationCoroutine()
    {
        int standart = healthManager.Defense;
        healthManager.Defense = healthManager.Defense * statEffects.PenetrationRate / 100;
        effectsImage[4].gameObject.SetActive(true);
        yield return new WaitForSeconds(statEffects.PenetrationTime);
        healthManager.Defense = standart;
        effectsImage[4].gameObject.SetActive(false);
    }
    public void Burn() => StartEffectCoroutine(() => BurnCoroutine(), 5);
    private IEnumerator BurnCoroutine()
    {
        int i = 0;
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
            i = 3;
        }
        WaitForSeconds timer = new WaitForSeconds(statEffects.BurnCD);
        int n = 0;
        effectsImage[5].gameObject.SetActive(true);
        while (statEffects.BurnCD * n < statEffects.BurnTime)
        {
            healthManager.CurrentHealth = healthManager.CurrentHealth - statEffects.BurnDMG[i];
            yield return timer;
            n++;
        }
        healthManager.CurrentHealth = healthManager.CurrentHealth - statEffects.BurnDMG[i];
        effectsImage[5].gameObject.SetActive(false);
    }
    public void Blindness() => StartEffectCoroutine(() => BlindnessCoroutine(), 6);
    private IEnumerator BlindnessCoroutine()
    {
        effectsImage[6].gameObject.SetActive(true);
        float standart1 = mainCamera[0].farClipPlane;
        float standart2 = mainCamera[1].farClipPlane;
        float standart3 = mainCamera[2].farClipPlane;
        for (int i = 0; i < 3; i++)
        {
            mainCamera[i].farClipPlane = mainCamera[i].farClipPlane * statEffects.BlindnessRate / 100;
        }
        yield return new WaitForSeconds(statEffects.BlindnessTime);
        mainCamera[0].farClipPlane = standart1;
        mainCamera[1].farClipPlane = standart2;
        mainCamera[2].farClipPlane = standart3;
        effectsImage[6].gameObject.SetActive(false);
    }
    public void Cursed() => StartEffectCoroutine(() => CursedCoroutine(), 7);
    private IEnumerator CursedCoroutine()
    {
        healthManager.cursed = true;
        effectsImage[7].gameObject.SetActive(true);
        yield return new WaitForSeconds(statEffects.CursedTime);
        healthManager.cursed = false;
        effectsImage[7].gameObject.SetActive(false);
    }
    public void Hex() => StartEffectCoroutine(() => HexCoroutine(), 8);
    private IEnumerator HexCoroutine()
    {
        healthManager.hex = statEffects.HexRate;
        effectsImage[8].gameObject.SetActive(true);
        yield return new WaitForSeconds(statEffects.HexTime);
        healthManager.hex = 1;
        effectsImage[8].gameObject.SetActive(false);
    }
    public void Regeneration() => StartEffectCoroutine(() => RegenerationCoroutine(), 9);
    private IEnumerator RegenerationCoroutine()
    {
        float timer = statEffects.RegenerationCD;
        int n = 0;
        effectsImage[9].gameObject.SetActive(true);
        while (statEffects.RegenerationCD * n < statEffects.RegenerationTime)
        {
            healthManager.GetHealth(statEffects.Regeneration);
            yield return timer;
            n++;
        }
        healthManager.GetHealth(statEffects.Regeneration);
        effectsImage[9].gameObject.SetActive(false);
    }
    public void Strength() => StartEffectCoroutine(() => StrengthCoroutine(), 10);

    private IEnumerator StrengthCoroutine()
    {
        effectsImage[10].gameObject.SetActive(true);
        StaticEffects.strengthRate = statEffects.StrengthRate;
        yield return new WaitForSeconds(statEffects.WeaknessTime);
        effectsImage[10].gameObject.SetActive(false);
        StaticEffects.strengthRate = statEffects.StrengthRate;
    }
    public void Speed() => StartEffectCoroutine(() => SpeedCoroutine(), 11);


    private IEnumerator SpeedCoroutine()
    {
        effectsImage[11].gameObject.SetActive(true);
        float speed = movementManager.Speed;
        movementManager.Speed = statEffects.SpeedRate * movementManager.Speed / 100;
        yield return new WaitForSeconds(statEffects.SpeedTime);
        movementManager.Speed = speed;
        effectsImage[11].gameObject.SetActive(false);
    }

    public void VampirismHP() => StartEffectCoroutine(() => VampirisimHPCoroutine(), 12);

    private IEnumerator VampirisimHPCoroutine()
    {
        StaticEffects.vampirismHP = true;
        effectsImage[12].gameObject.SetActive(true);
        yield return new WaitForSeconds(statEffects.VampirismHPTime);
        effectsImage[12].gameObject.SetActive(false);
        StaticEffects.vampirismHP = false;
    }

    public void VampirismHPLogic(int value)
    {
        healthManager.GetHealth(value * statEffects.VampirismHP / 100);
    }
    public void VampirismMana() => StartEffectCoroutine(() => VampirisimManaCoroutine(), 13);

    private IEnumerator VampirisimManaCoroutine()
    {
        StaticEffects.vampirismMana = true;
        effectsImage[13].gameObject.SetActive(true);
        yield return new WaitForSeconds(statEffects.VampirismManaTime);
        effectsImage[13].gameObject.SetActive(false);
        StaticEffects.vampirismMana = false;
    }

    public void VampirismManaLogic(int value)
    {
        manaManager.ReplenishMana(value * statEffects.VampirismMana / 100);
    }
    public void Resistance() => StartEffectCoroutine(() => ResistanceCoroutine(), 14);

    private IEnumerator ResistanceCoroutine()
    {
        effectsImage[14].gameObject.SetActive(true);
        int standart = healthManager.Defense;
        healthManager.Defense = (int)(statEffects.ResistanceRate * movementManager.Speed / 100);
        yield return new WaitForSeconds(statEffects.ResistanceTime);
        healthManager.Defense = standart;
        effectsImage[14].gameObject.SetActive(false);
    }
    public void Shocks() => StartEffectCoroutine(() => ShocksCoroutine(), 15);

    private IEnumerator ShocksCoroutine()
    {
        effectsImage[15].gameObject.SetActive(true);
        StaticEffects.shock = true;
        yield return new WaitForSeconds(statEffects.ShocksTime);
        StaticEffects.shock = false;
        effectsImage[15].gameObject.SetActive(false);
    }

}
