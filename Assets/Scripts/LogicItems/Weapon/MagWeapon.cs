using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagWeapon : WeaponParent
{
    [SerializeField] GameObject magicAttack;
    [SerializeField] Transform mainVirtualCamera;

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
        Vector3 spawnPosition = mainVirtualCamera.position + mainVirtualCamera.forward * 2.5f; spawnPosition.y = mainVirtualCamera.position.y;
        GameObject go = Instantiate(magicAttack, spawnPosition, mainVirtualCamera.transform.rotation);
        go.GetComponent<MagicAmmunitionLogic>().ifMagicSplash = dti.MagicSplash;
        go.GetComponent<MagicAmmunitionLogic>().timer = dti.HowMuch;
        go.GetComponent<MagicAmmunitionLogic>().speed = dti.Speed;
        go.GetComponent<MagicAmmunitionLogic>().attack = dti.Damage;
        go.SetActive(true);
    }
}
