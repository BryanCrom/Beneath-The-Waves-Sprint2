using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    // References to player and nav mesh agent
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    // Shooting properties
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRange = 10f;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public LayerMask obstacleMask;
    private float nextFireTime = 0f;

    // Health and damage properties
    public int enemyHealth = 100;
    public int damage = 10;
    public float attackCooldown = 2.0f;
    private bool canAttack = true;
    private bool isDead = false;
    public float attackRange = 1.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // If player is not assigned in the inspector, find it by tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Check if the enemy has line of sight to the player
            if (HasLineOfSight())
            {
                agent.isStopped = true; // Stop moving when within shooting range
                transform.LookAt(player);

                if (Time.time >= nextFireTime)
                {
                    ShootBullet();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                // If no line of sight, keep moving towards the player
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            // If out of range, keep moving towards the player
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        CheckPlayerInRange();
    }

    bool HasLineOfSight()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        if (Physics.Raycast(firePoint.position, directionToPlayer, out hit, shootingRange, obstacleMask))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    public void ShootBullet()
    {
        // Play shooting animation
        if (animator != null)
        {
            animator.SetTrigger("shoot");
        }

        // Instantiate and shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
    }

    private void CheckPlayerInRange()
    {
        if (isDead) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && canAttack)
            {
                Debug.Log("Player within attack range!");
                hit.GetComponent<Player>().takeDamage(damage);
                StartCoroutine(AttackCooldown());
                break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("death");
            StartCoroutine(DestroyAfterDelay(4f));
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
