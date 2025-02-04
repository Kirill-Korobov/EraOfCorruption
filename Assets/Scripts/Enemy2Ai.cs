using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Ai : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolRange = 10f;
    public Transform player;
    public float detectionRange = 15f;
    public float stopDistance = 10f;
    public Vector3 patrolTarget;
    public float chaseSpeed = 4f;
    private bool isChasing = false;
    public Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > stopDistance)
        {
            StartChasingPlayer();
        }
        else if (distanceToPlayer <= stopDistance)
        {
            StopAndAttack();
        }
        else
        {
            Patrol();
        }
    }

    void StartChasingPlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
    }

    void StopAndAttack()
    {
        
        Debug.Log("Enemy is attacking!");
    }

    void Patrol()
    {
        if (isChasing)
        {
            isChasing = false;
            
        }
        if (!isChasing)
        {
            SetNewPatrolTarget();
            Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);
        }
    }
    void SetNewPatrolTarget() {
        patrolTarget = startingPosition + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
