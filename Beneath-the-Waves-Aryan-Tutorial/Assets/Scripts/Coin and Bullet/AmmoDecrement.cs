using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDecrement : MonoBehaviour
{
    public int ammoCount = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoCount > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        ammoCount--;
        UIManager.SafeUpdateAmmoCount(ammoCount);

        Debug.Log("Shot fired! Ammo left: " + ammoCount);
    }
}
