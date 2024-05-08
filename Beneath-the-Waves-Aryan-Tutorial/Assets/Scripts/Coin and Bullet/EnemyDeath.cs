using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject coinPrefab;

    private void OnDestroy()
    {

        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}

