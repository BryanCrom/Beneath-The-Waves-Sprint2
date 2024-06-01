using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;


    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
