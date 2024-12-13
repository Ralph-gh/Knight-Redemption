using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Reference to the PauseMenu GameObject

    private static bool isPaused = false;

    public AudioMixerGroup SFXGroup;
    public AudioMixerGroup MusicGroup;
    AudioSource audioSource;
    public AudioClip buttonSound;
    public static bool IsGamePaused()
    {
        return isPaused;
    }

    private void Update()
    {
        // Toggle pause with "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true); // Show the pause menu
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Hide the pause menu
        }
    }

    public void playsound()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(buttonSound);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure the game is not paused when resetting
        SceneManager.LoadScene("TitleMenu"); // Load the Title Menu scene
        Debug.Log("Game reset to Title Menu");
    }
}
