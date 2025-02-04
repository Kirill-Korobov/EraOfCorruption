using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAi : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionRange = 5f;
    public float patrolRange = 10f;
    public Animator animator;
    public Rigidbody rb;
    private Vector3 startingPosition;
    private Vector3 patrolTarget;

    private void Start()
    {
        startingPosition = transform.position;
        SetNewPatrolTarget();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > 2.5f)
        {
            ChasePlayer();
            animator.SetTrigger("Idle");
            animator.SetTrigger("Run");
            transform.LookAt(player);
        }
        else if (distanceToPlayer <= 2.5f) {
            animator.SetTrigger("Idle");
            animator.SetTrigger("Attack1");
        }
        else
        {
            Patrol();
            animator.SetTrigger("Idle");
            animator.SetTrigger("Run");
            transform.LookAt(patrolTarget);
        }
        
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, patrolTarget) < 0.5f)
        {
            SetNewPatrolTarget();
        }
    }

    private void SetNewPatrolTarget()
    {
        patrolTarget = startingPosition + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    void LateUpdate() {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}

