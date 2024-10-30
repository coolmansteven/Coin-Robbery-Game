using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;         // Speed of the projectile
    private Vector3 direction;        // Direction of projectile travel
    private float boundaryPadding = 1f;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // Move the projectile in the set direction
        transform.position += direction * speed * Time.deltaTime;

        // Despawn the projectile if it goes off-screen
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOffScreen()
    {
        // Convert projectile position to screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        // Check if it's beyond the screen bounds with padding
        return screenPosition.x < -boundaryPadding ||
               screenPosition.x > Screen.width + boundaryPadding ||
               screenPosition.y < -boundaryPadding ||
               screenPosition.y > Screen.height + boundaryPadding;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // End the game if the player is hit
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.EndGame();
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
        else if (other.CompareTag("Walls")) // Optional: Destroy if it hits a wall or boundary
        {
            Destroy(gameObject);
        }
    }
}
