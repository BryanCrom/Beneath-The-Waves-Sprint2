using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject coinPrefab;
    public static event System.Action OnEnemyKilled; // Event for when an enemy is killed

    private bool isDead = false; // Flag to track if the enemy is already dead

    private void OnDestroy()
    {
        if (!isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            SpawnCoin();
            TriggerOnEnemyKilled();
        }
    }

    private void SpawnCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    private void TriggerOnEnemyKilled()
    {
        isDead = true; // Set the flag to indicate that the enemy is dead
        OnEnemyKilled?.Invoke();
    }
}
