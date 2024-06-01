using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //animations
    private Animator animator;
    private NavMeshAgent agent;

    public int enemyHealth = 100;
    public NavMeshAgent Agent { get => agent; }

    //enemy fix
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 2.0f;
    private bool canAttack = true;

    //check if enemy dead 
    //fixes the issue when player walkes close to the enemy's body and still takes damage
    private bool isDead = false;

    Transform player;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //initialise enemy manager
        //enemyManager = FindObjectOfType<EnemyManager>(); // Or assign it through inspector

    }

    private void Update()
    {
        CheckPlayerinRange();
    }
    public void takeDamage(int damage)
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

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void CheckPlayerinRange()
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

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
