using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrops : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAccount.AddBullets(20);
            Destroy(gameObject);
        }
    }
}
