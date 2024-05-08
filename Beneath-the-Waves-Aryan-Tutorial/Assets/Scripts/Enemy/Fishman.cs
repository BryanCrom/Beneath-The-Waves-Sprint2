using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishman : MonoBehaviour
{
    public FishmanHand hand;
    public int fishmanDamage;

    private void Start()
    {
        hand.damage = fishmanDamage;
    }
}
