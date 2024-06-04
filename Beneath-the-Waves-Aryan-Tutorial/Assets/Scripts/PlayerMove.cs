using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private CharacterController myCC;
    public float walkSpeed = 10f;
    public float runSpeed = 13f;
    public float jumpPower = 10f;

    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 inputVector;
    private Vector3 movementVector;
    [SerializeField]
    private float myGravity = 15f;

    private bool isRunning;

    public HumbleMovementCalc calculator;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        calculator = GetComponent<HumbleMovementCalc>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) && myCC.isGrounded || isRunning && !myCC.isGrounded;
        inputVector = transform.TransformDirection(calculator.CalcMovement(isRunning, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), walkSpeed, runSpeed));

        float movementDirectionY = movementVector.y;
        if (!myCC.isGrounded)
        {
            if (isRunning)
            {
                movementVector = 0.6f * movementVector + 0.4f * inputVector;
            }
            else
            {
                movementVector = 0.6f * inputVector - 0.6f * (Vector3.up * -myGravity);
            }
        } else
        {
            movementVector = inputVector + (Vector3.up * -myGravity);
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
            runSpeed = crouchSpeed;
            walkSpeed = crouchSpeed;

        }
        else
        {
            myCC.height = defaultHeight;
            walkSpeed = 10f;
            runSpeed = 13f;
        }

    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
}
