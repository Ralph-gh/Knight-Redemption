using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPoint = new Vector3(0, 0, 0); // Default respawn point
    [SerializeField] private int lives = 3; // Starting lives

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            RespawnOrGameOver();
        }
        if (collision.CompareTag("CheckPoint1"))
        {
            SceneManager.LoadScene("TitleMenu");
        }
    }
    public void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }
    private void RespawnOrGameOver()
    {
        lives--;

        if (lives <= 0)
        {
            GameOver();
           
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        Debug.Log($"Player respawned at {respawnPoint}. Lives left: {lives}");
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverMenu"); // Load the Game Over menu
    }

    public void ResetStats()
    {
        lives = 3; // Reset lives
        transform.position = respawnPoint; // Reset position
        Debug.Log("Player stats reset!");
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        Debug.Log($"Respawn point updated to {respawnPoint}");
    }
}
