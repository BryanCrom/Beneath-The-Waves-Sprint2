using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{

    private StateMach stateMach;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject player;

    public NavMeshAgent Agent { get { return agent; } }
    public GameObject Player { get { return player; } }

    [SerializeField]
    private string currentState;

    public int enemyHealth = 100;
    //path to follow
    public Path path;

    //for attack state
    public float sightDistance = 20f;
    public float fieldOfView = 85f;

    public Transform gunBarrel;
    public float fireRate = 2f;

    //private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        stateMach = GetComponent<StateMach>();
        agent = GetComponent<NavMeshAgent>();
        stateMach.Initialise(this);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer();
        currentState = stateMach.activeState.ToString();
    }


    public bool canSeePlayer()
    {
        if (player != null)
        {
            //if player is close enough to be seen by the enemy
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 target = player.transform.position - transform.position;
                float angle = Vector3.Angle(target, transform.forward);

                if (angle >= -fieldOfView && angle <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position, target);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit, sightDistance)) 
                    {
                        if (hit.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                }
            }
        }
        return false;
    }

    public void takeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            //isDead = true;
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


    //public void SetPatrolling(bool isPatrolling)
    //{
    //    animator.SetBool("IsPatrolling", isPatrolling);
    //}
    //public void SetAttacking(bool isShooting)
    //{
    //    animator.SetBool("IsShooting", isShooting);
    //}
}
