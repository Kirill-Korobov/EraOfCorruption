using System;
using UnityEngine;

public class PoisonLogic : MonoBehaviour
{
    [SerializeField] private DropedTakedItems dti;
    private Action[] randomEffects =
    {
            StaticEffects.Speed,
            StaticEffects.Regeneration,
            StaticEffects.Resistance,
            StaticEffects.Strength,
            StaticEffects.VampirismHP,
            StaticEffects.VampirismMana
    };
    private void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.attack) && (!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen))
        {
            switch (dti.PoisonsType)
            {
                case Poisons.Speed:
                    StaticEffects.Speed();
                    break;
                case Poisons.Regeneration:
                    StaticEffects.Regeneration();
                    break;
                case Poisons.Resistance:
                    StaticEffects.Resistance();
                    break;
                case Poisons.Strength:
                    StaticEffects.Strength();
                    break;
                case Poisons.VampirismHP:
                    StaticEffects.VampirismHP();
                    break;
                case Poisons.VampirismMana:
                    StaticEffects.VampirismMana();
                    break;
                case Poisons.Random:
                    randomEffects[UnityEngine.Random.Range(0, 6)]();
                    break;
            }
            if (StaticDropTake.sl.EatOrDrink())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
