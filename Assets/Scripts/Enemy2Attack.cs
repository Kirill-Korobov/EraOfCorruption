using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public Transform player;
    public float attackRange = 10f;
    public float attackRate = 1f;
    public int attackDamage = 10;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
<<<<<<< HEAD
        player.GetComponent<MC_HealthManager>().TakeDamage(attackDamage);
=======
        player.GetComponent<Player>().TakeDamage(attackDamage);
>>>>>>> 4124b93 (Add enemies, change enemy ai)
    }
}
