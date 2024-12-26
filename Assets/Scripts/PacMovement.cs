using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostSpeed = 10f;
    public float rotationSpeed = 5f;
    public Vector3 velocity;
    public Vector3 moveDirection;

    public bool isGrounded;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    public DynamicJoystick joystick; // Reference to the DynamicJoystick

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (joystick == null)
        {
            Debug.LogError("DynamicJoystick is not assigned. Please attach it in the Inspector.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset gravity effect when grounded
        }

        // Get input from joystick
        float lookx = joystick.Horizontal;
        float lookz = joystick.Vertical;

        moveDirection = new Vector3(lookx, 0, lookz);

        if (moveDirection.magnitude >= 0.1f)
        {
            // Rotate the character towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Adjust speed with boost
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? boostSpeed : moveSpeed;
        Vector3 motion = moveDirection * currentSpeed;
        controller.Move(motion * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
