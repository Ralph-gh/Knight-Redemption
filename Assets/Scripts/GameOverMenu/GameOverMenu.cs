using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your main game scene name
    }

    public void ReturnToTitleMenu()
    {
        SceneManager.LoadScene("TitleMenu"); // Replace with your title menu scene name
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToTitleMenu();
        }
    }
}
