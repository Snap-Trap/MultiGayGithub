using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcMovement : MonoBehaviour
{
    private CameraRotation cameraRotation;
        
    // Le input action variables
    public InputAction moveLeft, moveRight, moveForward, moveBackwards, jump;

    // Le basic variables
    public float speed, jumpForce, maxRayDistance, playerRotation;
    public bool isGrounded;

    private Rigidbody rb;

    private LayerMask whatIsGround;

    public void Start()
    {
        cameraRotation = GetComponent<CameraRotation>();

        // Assigns the input actions to the keys
        moveLeft = new InputAction("moveLeft", InputActionType.Button, "<Keyboard>/a");
        moveRight = new InputAction("moveRight", InputActionType.Button, "<Keyboard>/d");
        moveForward = new InputAction("moveForward", InputActionType.Button, "<Keyboard>/w");
        moveBackwards = new InputAction("moveBackwards", InputActionType.Button, "<Keyboard>/s");
        jump = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");

        rb = GetComponent<Rigidbody>();

        whatIsGround = LayerMask.GetMask("groundLayer");

        speed = 5;
        jumpForce = 5;
        maxRayDistance = 1.2f;
    }

    public void Update()
    {
        // Not so basic camera movement
        // Makes it so the other script that controls the camera gives the rotation and you can use it here so forward goes forward based on--
        // what the player's forward is instead of the scene's forward
        // Touch this and I'll break your kneecaps
        playerRotation = cameraRotation.turn.x;

        Quaternion playerRotationQuaternion = Quaternion.Euler(0, playerRotation, 0);

        Vector3 forwardMovement = playerRotationQuaternion * Vector3.forward;
        Vector3 rightMovement = playerRotationQuaternion * Vector3.right;

        // Basic movement
        Vector3 movement = Vector3.zero;

        if (moveLeft.ReadValue<float>() == 1)
        {
            Debug.Log("Moving left");
            movement += -rightMovement * speed;
        }
        if (moveRight.ReadValue<float>() == 1)
        {
            Debug.Log("Moving right");
            movement += rightMovement * speed;
        }
        if (moveForward.ReadValue<float>() == 1)
        {
            Debug.Log("Moving forward");
            movement += forwardMovement * speed;
        }
        if (moveBackwards.ReadValue<float>() == 1)
        {
            Debug.Log("Moving backwards");
            movement += -forwardMovement * speed;
        }

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);


        // Jumping

        if (isGrounded && jump.ReadValue<float>() == 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

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

    public void OnEnable()
    {
        moveLeft.Enable();
        moveRight.Enable();
        moveForward.Enable();
        moveBackwards.Enable();
        jump.Enable();
    }

    public void OnDisable()
    {
        moveLeft.Disable();
        moveRight.Disable();
        moveForward.Disable();
        moveBackwards.Disable();
        jump.Disable();
    }
}
