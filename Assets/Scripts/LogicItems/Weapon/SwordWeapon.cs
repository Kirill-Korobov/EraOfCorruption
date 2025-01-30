using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponParent
{
    [SerializeField] private Transform virtualCamera;
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
        if (dti.Splash)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < go.Length; i++)
            {
                Vector3 directionToEnemy = (go[i].transform.position - transform.position).normalized;
                Vector3 forward = transform.forward;

                float dotProduct = Vector3.Dot(forward, directionToEnemy);
                float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

                if (angle <= dti.AttackAngle / 2)
                {
                    if (StaticEffects.vampirismHP)
                    {
                        StaticEffects.VampirismHPLogic(dti.Damage);
                    }
                    if (StaticEffects.vampirismMana)
                    {
                        StaticEffects.VampirismManaLogic(dti.Damage);
                    }
                    go[i].GetComponent<AttackEnemy>().EnemyAttaked(dti.Damage);
                }
            }
        }
        {
            Debug.DrawRay(virtualCamera.position, virtualCamera.forward, Color.red, 20);
            Debug.Log(dti.Range);
            if (Physics.Raycast(virtualCamera.position, virtualCamera.forward, out RaycastHit hit, dti.Range))
            {
                Debug.Log(2);
                AttackEnemy ti;
                if (hit.collider.gameObject.TryGetComponent(out ti))
                {
                    ti.EnemyAttaked(dti.Damage);
                }
            }
        }
    }
    
}
