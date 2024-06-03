using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class schoolMovement : MonoBehaviour
{
    public GameObject fishMissle;
    public float turnSpeed = 45.0f;
    public float forwardSpeed = 0.1f;
    public float smoothing = 0.8f;
    private float previousTurn = 0.0f;
    public GameObject player;
    private bool fired = false;
    private int health = 10;
    private float scale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        previousTurn = previousTurn * smoothing + turnSpeed * (Random.value * 2.0f - 1.0f) * (1 - smoothing);
        transform.Rotate(0.0f, previousTurn, 0.0f, Space.Self);
        transform.position += transform.forward * forwardSpeed;
        //if (!fired) {
            //FishM clone = Instantiate<FishM>(fishMissle);
            //clone.setTarget(player);
        //}
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        scale = (float)health / 10.0f;
        transform.localScale = transform.localScale * scale;
    }
}
