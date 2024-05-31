using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackState : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private ShootingEnemy shootingEnemy;
    public float stopAttacking = 2.5f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        shootingEnemy = animator.GetComponent<ShootingEnemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer > stopAttacking)
        {
            animator.SetBool("isAttacking", false);
        }
        else if (Time.time >= nextFireTime)
        {
            shootingEnemy.ShootBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
