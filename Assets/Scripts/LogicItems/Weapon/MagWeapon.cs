using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagWeapon : WeaponParent
{
    [SerializeField] GameObject magicAttack;
    [SerializeField] Transform maInVirtualCamera;

    private void Update()
    {
        
        if (Input.GetKeyDown(LoadedSettings.attack) && !LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
        {
            if (attack && dti.ManaCost <= MC_ManaManager.Mana)
            {
                MC_ManaManager.SpendMana(dti.ManaCost);
                Attack();
                StartCoroutine(Reload(dti.Reload));
            }
        }
    }
    public override void Attack()
    {
        Debug.Log(1);
        Vector3 spawnPosition = maInVirtualCamera.position + maInVirtualCamera.forward * 2.5f; spawnPosition.y = maInVirtualCamera.position.y;
        GameObject go = Instantiate(magicAttack, spawnPosition, maInVirtualCamera.transform.rotation);
        go.GetComponent<MagicAmmunitionLogic>().ifMagicSplash = dti.MagicSplash;
        go.GetComponent<MagicAmmunitionLogic>().timer = dti.HowMuch;
        go.GetComponent<MagicAmmunitionLogic>().speed = dti.Speed;
        go.GetComponent<MagicAmmunitionLogic>().attack = dti.Damage;
        go.SetActive(true);
    }
}
