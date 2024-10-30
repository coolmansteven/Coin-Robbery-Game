using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            // Play the coin collection sound effect
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                Debug.Log("Playing sound!"); // Confirm sound is triggered
                audioSource.Play();
            }

            // Disable the coin's visuals and collider to avoid re-triggering
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            // Find the GameManager and PlayerMovement3D components
            GameManager gameManager = FindObjectOfType<GameManager>();
            PlayerMovement3D player = other.GetComponent<PlayerMovement3D>();

            // Update the score and player speed if both components are found
            if (gameManager != null && player != null)
            {
                gameManager.CollectCoin(player);
            }

            // Delay the destruction of the coin to allow the sound to finish playing
            if (audioSource != null && audioSource.clip != null)
            {
                Destroy(gameObject, audioSource.clip.length); // Destroy after audio completes
            }
            else
            {
                Destroy(gameObject); // Immediate destroy if no audio
            }
        }
    }
}

