using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private CharacterController myCC;
    public float speed;
    public float walkSpeed = 6f;
    public float runSpeed = 9f;
    public float jumpPower = 3f;

    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 inputVector;
    private Vector3 movementVector;
    [SerializeField]
    private float myGravity = 15f;

    private bool isRunning;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    void Update()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) && myCC.isGrounded || isRunning && !myCC.isGrounded;
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        float movementDirectionY = movementVector.y;
        if (myCC.isGrounded)
        {
            if (isRunning)
            {
                speed = runSpeed;
                movementVector = (inputVector * speed) + (Vector3.up * -myGravity);
            }
            else
            {
                speed = walkSpeed;
                movementVector = (inputVector * speed) + (Vector3.up * -myGravity);
            }
        }
        else
        {
            if (isRunning)
            {
                speed = runSpeed;
                movementVector = 0.6f * movementVector + 0.4f * (inputVector * speed) + (Vector3.up * -myGravity);
            }
            else
            {
                speed = walkSpeed;
                movementVector = 0.6f * (inputVector * speed) + 0.4f * (Vector3.up * -myGravity);
            }
        }

        // Jumping
        if (Input.GetButton("Jump") && myCC.isGrounded)
        {
            movementVector.y = jumpPower;
        }
        else
        {
            movementVector.y = movementDirectionY;
        }

        // In the air
        if (!myCC.isGrounded)
        {
            movementVector.y -= myGravity * Time.deltaTime;
        }

        // Crouching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            myCC.height = crouchHeight;
            speed = crouchSpeed;

        }
        else
        {
            myCC.height = defaultHeight;
            speed = walkSpeed;
        }

    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
}
