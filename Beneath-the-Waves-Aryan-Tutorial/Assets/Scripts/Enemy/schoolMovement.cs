using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SchoolMovement : MonoBehaviour
{
    public float turnSpeed = 45.0f;
    public float forwardSpeed = 0.1f;
    public float smoothing = 0.8f;
    private float previousTurn = 0.0f;
    private int health = 5;

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
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        transform.localScale = transform.localScale * (1f - ((5f - health) * 0.2f));
        Debug.Log(health);
        Debug.Log((1f - ((5f - health) * 0.2f)));
        if (health == 1)
        {
            Destroy(gameObject);
        }
    }
}
