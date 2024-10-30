using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : MonoBehaviour
{
    public GameObject projectilePrefab;      // Reference to the projectile prefab
    public Transform player;                 // Reference to the player
    private float shootInterval;             // Time between shots
    private AudioSource audioSource;         // Reference to the AudioSource component

    private Animator animator;               // Reference to the Animator on the subchild

    void Start()
    {
        // Set a random shoot interval between 1 and 5 seconds
        SetRandomShootInterval();
        // Begin shooting at intervals
        InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        animator = GetComponentInChildren<Animator>();  // Get Animator from the subchild
    }

    void SetRandomShootInterval()
    {
        shootInterval = Random.Range(1f, 5f);
    }

    void Shoot()
    {
    if (player == null) return;  // Ensure the player is still active in the game

    // Play the fire animation
    if (animator != null)
    {            
        animator.SetTrigger("FireTrigger");
    }

    // Calculate the direction from the officer to the player
    Vector3 direction = (player.position - transform.position).normalized;

    // Instantiate a new projectile and rotate it to face the player
    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(direction));
    
    // Assign the direction to the projectile's movement script
    Projectile projectileScript = projectile.GetComponent<Projectile>();
    if (projectileScript != null)
    {
        projectileScript.SetDirection(direction);
    }

    // Play the shooting sound effect
    if (audioSource != null && audioSource.clip != null)
    {
        audioSource.Play();
    }

    // Set a new random shoot interval
    SetRandomShootInterval();
    }

}
