using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene"); // Replace with your main game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has exited");

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has exited"); // For debugging in the editor
    }
}
