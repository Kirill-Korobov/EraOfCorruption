using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

public class BowWeapon : WeaponParent
{
    [SerializeField] ArrowArray arrows;
    [SerializeField] Transform mainVirtualCamera;
    private int i;
    private void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.attack) && !LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen && attack && dti.ManaCost <= MC_ManaManager.Mana)
        {
            Debug.Log(12);
            bool t = StaticDropTake.sl.ArrowUse();
            if (attack && dti.ManaCost <= MC_ManaManager.Mana && t)
            {
                Debug.Log(1);
                MC_ManaManager.SpendMana(dti.ManaCost);
                Attack();
                StartCoroutine(Reload(dti.Reload));
            }
        }
    }
    public override void Attack()
    {
        Debug.Log(1);
        for (int i = 0; i < arrows.Arrows.Length; i++)
        {
            if (StaticDropTake.sl.dtiArrow.ID == arrows.Arrows[i].GetComponent<ScriptableObjectUsedItems>().dti.ID)
            {
                this.i = i;
                break;
            }
        }
        Vector3 spawnPosition = mainVirtualCamera.position + mainVirtualCamera.forward * 2.5f; spawnPosition.y = mainVirtualCamera.position.y;
        GameObject go = Instantiate(arrows.Arrows[i], spawnPosition, mainVirtualCamera.transform.rotation);
        go.GetComponent<ArrowLogic>().range = dti.HowMuch;
        go.GetComponent<ArrowLogic>().speed = dti.Speed;
        go.GetComponent<ArrowLogic>().attack = dti.Damage + arrows.Arrows[i].GetComponent<ScriptableObjectUsedItems>().dti.ArrowDamage;
        go.SetActive(true);
    }
}
