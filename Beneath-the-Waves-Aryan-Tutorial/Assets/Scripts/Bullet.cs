using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            //test if the bullet hits
            print("hit" + collision.gameObject.name);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            //BulletImpactEffect(collision);
            Destroy(gameObject);
        }
       
    }
}
