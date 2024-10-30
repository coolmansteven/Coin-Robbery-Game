using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reference to the TextMesh component for displaying score
    public TextMesh scoreTextMesh;
    public int playerScore = 0;            // Track the player's score
    public float speedIncrease = 0.5f;     // Amount to increase player speed per coin

    // UI Elements for Start and Game Over Screens
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public Text gameOverScoreText;     // Display final score in Game Over screen

    //private AudioSource audioSource;         // Reference to AudioSource for game over sound

    private AudioSource gameOverSoundSource;  // Reference to AudioSource for game over sound
    private AudioSource backgroundMusicSource; // Reference to AudioSource for background music

    

    void Start()
    {
        // Show Start Screen at launch, hide Game Over screen, and pause game
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0;  // Pauses the game at start

        // Get the AudioSource component
        //audioSource = GetComponent<AudioSource>();

        // Get the AudioSource components, assuming [0] is game over sound and [1] is background music
        AudioSource[] audioSources = GetComponents<AudioSource>();
        gameOverSoundSource = audioSources[0];       // Game Over sound
        backgroundMusicSource = audioSources[1];     // Background music
        backgroundMusicSource.loop = true;           // Enable looping for background music
    }

    // Called when the player clicks "Start Game"
    public void StartGame()
    {
        startPanel.SetActive(false);    // Hide Start screen
        playerScore = 0;                // Reset score
        UpdateScoreUI();                // Initialize score display
        Time.timeScale = 1;             // Start the game

        // Start playing the background music
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    // Method to handle coin collection, update score, and increase player speed
    public void CollectCoin(PlayerMovement3D player)
    {
        playerScore += 10;                   // Increase score by 10 points
        player.IncreaseSpeed(speedIncrease);  // Increase player speed
        UpdateScoreUI();                      // Update the score display
    }

    // Method to update the score display
    private void UpdateScoreUI()
    {
        scoreTextMesh.text = "Score: " + playerScore;
    }

    // Method to end the game and show Game Over screen
    public void EndGame()
    {
        // Show Game Over screen, display score, and freeze the game
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Final Score: " + playerScore;
        Time.timeScale = 0;  // Pause the game
        Debug.Log("Game Over! You’ve been arrested.");

         // Stop background music
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        // Play the game-over sound effect
        if (gameOverSoundSource != null && gameOverSoundSource.clip != null)
        {
            gameOverSoundSource.Play();
        }

    }

    // Method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
