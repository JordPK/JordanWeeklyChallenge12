using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    float normalMoveSpeed;
    float sprintSpeed;
    [SerializeField] float sprintSpeedMultiplier;
    [SerializeField] float jumpForce = 5f;
    public CharacterController characterController;
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform gun;

    [Header("Physics Settings")]
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        sprintSpeed = moveSpeed * sprintSpeedMultiplier;
        normalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        Sprint();
        
    }

    private void Move()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the player grounded
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = normalMoveSpeed;
        } 
    }
}