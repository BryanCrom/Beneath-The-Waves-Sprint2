using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("EnemyHand"))
        {
            print("Player is colliding w enemyhand");
            takeDamage(other.gameObject.GetComponent<FishmanHand>().damage);
        }
    }
}
