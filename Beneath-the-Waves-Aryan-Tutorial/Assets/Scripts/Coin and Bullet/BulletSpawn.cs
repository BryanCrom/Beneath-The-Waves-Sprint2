using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int maxBullets = 5;         // Maximum number of bullets to spawn
    public Vector3 spawnArea;          // Area around the player where bullets can spawn
    public GameObject player;          // Reference to the player GameObject

    private int bulletsSpawned = 0;    // Counter for the number of bullets spawned

    //private List<GameObject> spawnedBullets = new List<GameObject>(); // List to keep track of spawned bullets

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Use "Player" with an uppercase "P"
        if (player == null)
        {
            Debug.LogError("Player object not found, check the player tag.");
            this.enabled = false;
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned in the inspector.");
            this.enabled = false;
            return;
        }

        StartCoroutine(SpawnBulletsPeriodically());
    }


    IEnumerator SpawnBulletsPeriodically()
    {
        while (bulletsSpawned < maxBullets)
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet prefab is destroyed or not assigned.");
                yield break; // Exit the coroutine if the prefab is null
            }

            if (player == null)
            {
                Debug.LogError("Player object has been destroyed or not found.");
                yield break; // Exit the coroutine if the player is null
            }

            Debug.Log($"Spawning bullet {bulletsSpawned + 1}");
            Vector3 spawnPosition = player.transform.position + new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                0,
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );
            //GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            //spawnedBullets.Add(bullet); // Add spawned bullet to the list
            bulletsSpawned++;
            yield return new WaitForSeconds(10); // Wait for 10 seconds before spawning the next bullet
        }
        Debug.Log("Finished spawning bullets");
        this.enabled = false; // Optionally disable this component if no more spawning is needed
    }

    //private void OnDisable()
    //{
    //    // Destroy all spawned bullets when the scene is unloaded
    //    foreach (GameObject bullet in spawnedBullets)
    //    {
    //        Destroy(bullet);
    //        Debug.Log("bullets are destroyed");
    //    }
    //}

}

