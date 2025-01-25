using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponParent
{
    public override void BowUse() { }
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
                    go[i].GetComponent<AttackEnemy>().EnemyAttaked(dti.Damage);
                }
            }
        }
        else
        {
            RaycastHit hit;
            Transform go = GetComponentInParent<Transform>();
            if (Physics.Raycast(go.position, go.forward, out hit, dti.Range))
            {
                AttackEnemy ti;
                if (hit.collider.gameObject.TryGetComponent(out ti))
                {
                    ti.EnemyAttaked(dti.Damage);
                }
            }
        }
    }
    
}
