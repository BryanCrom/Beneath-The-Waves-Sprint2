using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly int MAXHP;
    public int HP = 100;

    public void takeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            print("YOU ARE DEAD!");
        }
        else
        {
            print("HIT!");
        }
    }
    public void healDamage(int heal)
    {
        HP += heal;
        if(HP > 100)
        {
            HP = 100;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("EnemyHand"))
        {
            print("Player is colliding w enemyhand");
            takeDamage(other.gameObject.GetComponent<FishmanHand>().damage);
        }
    }

    public int getHP()
    {
        return HP;
    }

    public int getMAXHP()
    {
        return MAXHP;
    }
}
