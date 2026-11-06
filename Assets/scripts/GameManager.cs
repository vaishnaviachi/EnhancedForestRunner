
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel;

    [Header("Live Counters")]
    public GameObject scoreText;
    public GameObject distanceText;

    [Header("Score Manager")]
    public ScoreManager scoreManager;

    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (scoreText != null)
            scoreText.SetActive(true);

        if (distanceText != null)
            distanceText.SetActive(true);
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        // Update final score and distance
        if (scoreManager != null)
            scoreManager.ShowFinalScore();

        // Hide live counters
        if (scoreText != null)
            scoreText.SetActive(false);

        if (distanceText != null)
            distanceText.SetActive(false);

        // Show Game Over screen
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
