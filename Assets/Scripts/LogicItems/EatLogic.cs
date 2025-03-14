using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatLogic : MonoBehaviour
{
    [SerializeField] private DropedTakedItems dti;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    private void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.attack) && statisticsInfo.SatietyMaxValue != satietyManager.Satiety && (!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)) 
        {
            satietyManager.ReplenishSatiety(dti.Nutrition);
            if (StaticDropTake.sl.EatOrDrink())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
