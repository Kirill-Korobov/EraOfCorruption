using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticEffects
{
    public static int weaknessRate;
    public static int strengthRate;
    public static bool vampirismHP;
    public static bool vampirismMana;
    public static float vampirismHPRate;
    public static float hunger;
    public static bool shock;
    public static EffectsCoroutine coroutines;

    public static void Poison()
    {
        coroutines.Poison();
    }
    public static void Weakness()
    {
        coroutines.Weakness();
    }
    public static void Slowness()
    {
        coroutines.Slowness();
    }
    public static void Hunger()
    {
        coroutines.Hunger();
    }
    public static void Penetration()
    {
        coroutines.Partialpenetration();
    }
    public static void Blindness()
    {
        coroutines.Blindness();
    }
    public static void Cursed()
    {
        coroutines.Cursed();
    }
    public static void Regeneration()
    {
        coroutines.Regeneration();
    }
    public static void Strength()
    {
        coroutines.Strength();
    }
    public static void Speed()
    {
        coroutines.Speed();
    }
    public static void VampirismHP()
    {
        coroutines.VampirismHP();
    }
    public static void VampirismHPLogic(int value)
    {
        coroutines.VampirismHPLogic(value);
    }
    public static void VampirismMana()
    {
        coroutines.VampirismMana();
    }
    public static void VampirismManaLogic(int value)
    {
        coroutines.VampirismManaLogic(value);
    }
    public static void Resistance()
    {
        coroutines.Resistance();
    }

    public static void Save()
    {
        coroutines.Save();
    }
}
