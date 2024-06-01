using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAttack : BaseState
{
    private float strafe;
    private float losePlayer;

    //shooting
    private float shotTimer;

    public override void Enter()
    {
        //shootingEnemy.SetAttacking(true);
    }

    public override void Exit()
    {
        //shootingEnemy.SetAttacking(false);
    }

    public override void Perform()
    {
       if (shootingEnemy.canSeePlayer())
        {
            losePlayer = 0;
            strafe += Time.deltaTime;
            shotTimer += Time.deltaTime;
            shootingEnemy.transform.LookAt(shootingEnemy.Player.transform);
            if (shotTimer > shootingEnemy.fireRate)
            {
                Shoot();
            }
            if ( strafe > Random.Range(3, 7 )) 
            {
                shootingEnemy.Agent.SetDestination(shootingEnemy.transform.position + (Random.insideUnitSphere * 5));
                strafe = 0;
            }
        }
       else
        {
            losePlayer += Time.deltaTime;
            if (losePlayer > 8)
            {
                //search state
                stateMach.ChangeState(new NewPatrol());
            }
        }
    }
    public void Shoot()
    {
        //reference to gun and instantiate new bullet
        Transform gunBarrel = shootingEnemy.gunBarrel;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet") as GameObject, gunBarrel.position, shootingEnemy.transform.rotation);

        //calculate direction to player
        Vector3 shootDir = (shootingEnemy.Player.transform.position - gunBarrel.transform.position).normalized;

        //add force rigidbody
        bullet.GetComponent<Rigidbody>().velocity = shootDir * 40;
        Debug.Log("Shoot");
        shotTimer = 0;



    }
}
