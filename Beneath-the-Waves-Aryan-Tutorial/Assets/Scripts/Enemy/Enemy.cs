using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //animations
    private Animator animator;
    private NavMeshAgent agent;

    public int enemyHealth = 100;
    public NavMeshAgent Agent { get => agent; }


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //initialise enemy manager
        //enemyManager = FindObjectOfType<EnemyManager>(); // Or assign it through inspector
    }


    public void takeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 2.5f);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, 5f);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, 7.5f);
    //}

}
