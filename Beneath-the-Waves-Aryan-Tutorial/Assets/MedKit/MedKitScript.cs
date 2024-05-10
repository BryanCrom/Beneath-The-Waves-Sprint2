using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitScript : MonoBehaviour
{
    private int heal = 33;
    private bool destroyed = false;

    private Player player;
    private int HP;
    private int MAXHP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        MAXHP = player.getMAXHP();
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        HP = player.getHP();
        if (HP < MAXHP)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player" && destroyed == false && HP < MAXHP)
        {
            Destroy(gameObject);
            destroyed = true;
            player.healDamage(heal);
        }
    }
}
