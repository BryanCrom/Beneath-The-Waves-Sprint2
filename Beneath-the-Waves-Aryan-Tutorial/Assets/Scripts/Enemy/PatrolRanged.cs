using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStateRanged : StateMachineBehaviour
{
    float timer;
    public float patrolTime = 10f;

    // Reference to the player and the NavMeshAgent
    Transform player;
    NavMeshAgent agent;

    // Patrol speed
    public float patrolSpeed = 2f;

    // Waypoints for patrolling
    List<Transform> waypoints = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;

        // Moving through waypoints
        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypoints.Add(t);
        }
        Vector3 nextPos = waypoints[Random.Range(0, waypoints.Count)].position;
        agent.SetDestination(nextPos);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the enemy has reached the waypoint
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }

        // Back to idle state
        timer += Time.deltaTime;
        if (timer > patrolTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        // Check if the player is within shooting range
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer <= animator.GetComponent<ShootingEnemy>().shootingRange)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
