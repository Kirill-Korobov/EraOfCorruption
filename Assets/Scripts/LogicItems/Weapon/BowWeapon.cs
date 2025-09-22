using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

public class BowWeapon : WeaponParent
{
    [SerializeField] ArrowArray arrows;
    [SerializeField] Transform maInVirtualCamera;
    private int i;
    private void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.attack) && !LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen && attack && dti.ManaCost <= MC_ManaManager.Mana)
        {
            bool t = StaticDropTake.sl.ArrowUse();
            if (attack && dti.ManaCost <= MC_ManaManager.Mana && t)
            {
                Attack();
                StartCoroutine(Reload(dti.Reload));
            }
        }
    }
    public override void Attack()
    {
        for (int i = 0; i < arrows.Arrows.Length; i++)
        {
            if (StaticDropTake.sl.dtiArrow.ID == arrows.Arrows[i].GetComponent<ScriptableObjectUsedItems>().dti.ID)
            {
                this.i = i;
                break;
            }
        }
        Vector3 spawnPosition = maInVirtualCamera.position + maInVirtualCamera.forward * 2f; 
        spawnPosition.y = maInVirtualCamera.position.y;
        GameObject go = Instantiate(arrows.Arrows[i], spawnPosition, maInVirtualCamera.transform.rotation);
        go.GetComponent<ArrowLogic>().range += dti.HowMuch;
        go.GetComponent<ArrowLogic>().speed += dti.Speed;
        go.GetComponent<ArrowLogic>().attack += dti.Damage + arrows.Arrows[i].GetComponent<ScriptableObjectUsedItems>().dti.ArrowDamage;
        go.SetActive(true);
    }
}
