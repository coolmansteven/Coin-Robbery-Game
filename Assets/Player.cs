using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    
    public float moveSpeed = 5f;  // Speed of the player movement
    private Rigidbody rb;
    private Vector3 movement;

    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;

    private Animator animator;     // Reference to Animator on the subchild
    private Transform playerSubchild; // Reference to the subchild for flipping


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate screen boundaries based on the camera's orthographic size and aspect ratio
        Camera mainCamera = Camera.main;
        float camHeight = mainCamera.orthographicSize * 2;
        float camWidth = camHeight * mainCamera.aspect;

        // Set boundaries based on camera size
        screenLeft = -camWidth / 2;
        screenRight = camWidth / 2;
        screenTop = camHeight / 2;
        screenBottom = -camHeight / 2;

        animator = GetComponentInChildren<Animator>(); // Get Animator from the subchild
        playerSubchild = animator.transform; // Reference to the subchild for flipping
    }

    void Update()
    {
        // Get input for X and Y movement (horizontal and vertical)
        float moveX = Input.GetAxisRaw("Horizontal");  // Left and Right (X axis)
        float moveY = Input.GetAxisRaw("Vertical");    // Up and Down (Y axis)

        // Combine into a movement vector for X and Y movement
        movement = new Vector3(moveX, moveY, 0f).normalized;

        // Set the Speed parameter based on movement magnitude
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }

        // Flip the character based on movement direction
        if (moveX < 0)
        {
            playerSubchild.localRotation = Quaternion.Euler(0, 180, 0); // Face left
        }
        else if (moveX > 0)
        {
            playerSubchild.localRotation = Quaternion.Euler(0, 0, 0); // Face right
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Check if the player is out of bounds and wrap accordingly
        WrapPosition();
    }

    void WrapPosition()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x > screenRight)  // Right side
        {
            newPosition.x = screenLeft;
        }
        else if (newPosition.x < screenLeft)  // Left side
        {
            newPosition.x = screenRight;
        }

        if (newPosition.y > screenTop)  // Top side
        {
            newPosition.y = screenBottom;
        }
        else if (newPosition.y < screenBottom)  // Bottom side
        {
            newPosition.y = screenTop;
        }

        // Apply the wrapped position to the player
        transform.position = newPosition;
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
