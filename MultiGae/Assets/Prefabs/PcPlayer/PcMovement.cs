using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcMovement : MonoBehaviour
{
    // Le input action variables
    private InputAction moveLeft, moveRight, moveForward, moveBackwards, jump;


    // Le basic variables
    public float speed, jumpForce, maxRayDistance;
    public bool isGrounded;

    private Rigidbody rb;

    private LayerMask whatIsGround;

    public void Start()
    {
        // Assigns the input actions to the keys
        moveLeft = new InputAction("MoveLeft", InputActionType.Button, "<Keyboard>/a");
        moveRight = new InputAction("MoveRight", InputActionType.Button, "<Keyboard>/d");
        moveForward = new InputAction("MoveForward", InputActionType.Button, "<Keyboard>/w");
        moveBackwards = new InputAction("MoveBackwards", InputActionType.Button, "<Keyboard>/s");
        jump = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");

        rb = GetComponent<Rigidbody>();

        whatIsGround = LayerMask.GetMask("groundLayer");

        speed = 5;
        jumpForce = 5;
        maxRayDistance = 1.2f;
    }

    public void Update()
    {
        // Basic movement

        if (moveLeft.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.left * speed);
        }
        if (moveRight.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.right * speed);
        }
        if (moveForward.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.forward * speed);
        }
        if (moveBackwards.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.back * speed);
        }

        if (isGrounded && jump.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Jumping

        if (Physics.Raycast(transform.position, Vector3.down, maxRayDistance, whatIsGround))
        {
            Debug.Log("Hitting the ground");
            isGrounded = true;
        }
        else
        {
            Debug.Log("Weeeeeeeeeee, I'm flying");
            isGrounded = false;
        }

        Debug.DrawRay(transform.position, Vector3.down * maxRayDistance, Color.red);
    }

    // OnEnable and OnDisable because for some fucking god damn reason you have to use them

    private void OnEnable()
    {
        moveLeft.Enable();
        moveRight.Enable();
        moveForward.Enable();
        moveBackwards.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        moveLeft.Disable();
        moveRight.Disable();
        moveForward.Disable();
        moveBackwards.Disable();
        jump.Disable();
    }
}
