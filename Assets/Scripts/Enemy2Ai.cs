using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Ai : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;

    public Transform player;
    public float detectionRange = 10f;
    public float stopDistance = 5f;

    public float chaseSpeed = 4f;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
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
            agent.speed = chaseSpeed;
        }

        agent.SetDestination(player.position);
    }

    void StopAndAttack()
    {
        agent.isStopped = true;
        Debug.Log("Enemy is attacking!");
    }

    void Patrol()
    {
        if (isChasing)
        {
            isChasing = false;
            agent.speed = patrolSpeed;
        }

        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
