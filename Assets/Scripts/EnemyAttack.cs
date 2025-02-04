using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Player player;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    public Animator animator;
    private float lastAttackTime = 0f;

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            TryAttack();
            animator.SetTrigger("Idle");
            animator.SetTrigger("Attack1");
        }
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Player _player = player.GetComponent<Player>();
            if (_player != null && _player.health > 0)
            {
                _player.TakeDamage(attackDamage);
            }

            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
