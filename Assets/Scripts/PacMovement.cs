using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public float moveSpeed = 9f; // Varsayýlan hareket hýzý
    public float boostSpeed = 15f; // Boost hareket hýzý
    public float rotationSpeed = 720f; // Döndürme hýzý
    public float gravity = -9.8f; // Yerçekimi kuvveti
    public float groundCheckDistance = 0.2f; // Yere yakýnlýk kontrol mesafesi
    public LayerMask groundMask; // Zemin katmaný
    public DynamicJoystick joystick; // Dinamik joystick referansý

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (joystick == null)
        {
            Debug.LogError("Joystick referansý atanmadý. Lütfen Inspector'dan atanmasýný saðlayýn.");
        }
    }

    void Update()
    {
        CheckGrounded();
        HandleMovement();
        ApplyGravity();
    }

    void CheckGrounded()
    {
        // Karakterin zeminde olup olmadýðýný kontrol eder
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zemine yapýþmayý saðlamak için küçük bir negatif deðer
        }
    }

    void HandleMovement()
    {
        // Joystick girdilerini alýr
        float inputX = joystick.Horizontal; // Yatay joystick girdisi
        float inputZ = joystick.Vertical; // Dikey joystick girdisi

        // Hareket yönünü hesapla
        Vector3 inputDirection = new Vector3(inputX, 0, inputZ).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Karakterin bakýþ yönünü ayarla
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Hareket yönünü belirle
            moveDirection = inputDirection * (Input.GetKey(KeyCode.LeftShift) ? boostSpeed : moveSpeed);
        }
        else
        {
            moveDirection = Vector3.zero; // Eðer joystick sýfýrsa karakter durur
        }

        // Karakteri hareket ettir
        controller.Move(moveDirection * Time.deltaTime);
    }

    void ApplyGravity()
    {
        // Yerçekimi uygula
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
