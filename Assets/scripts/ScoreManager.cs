
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Live UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinCountText;

    [Header("Game Over UI")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalDistanceText;

    [Header("Scoring")]
    public float pointsPerMeter = 1f;

    private float startingZ;
    private int currentScore;
    private int currentDistance;
    private int coinBonusScore = 0;
    private int coinsCollected = 0;

    void Start()
    {
        if (player == null || scoreText == null || distanceText == null)
        {
            Debug.LogError("Assign Player, ScoreText and DistanceText!");
            enabled = false;
            return;
        }

        startingZ = player.position.z;
        UpdateCoinCount();
    }

    void Update()
    {
        float distance = Mathf.Max(0f, player.position.z - startingZ);

        currentDistance = Mathf.FloorToInt(distance);
        currentScore = Mathf.FloorToInt(distance * pointsPerMeter) + coinBonusScore;

        scoreText.text = "Score: " + currentScore;
        distanceText.text = "Distance: " + currentDistance + " m";
    }

    public void AddCoinPoints(int points)
    {
        coinsCollected++;
        coinBonusScore += points;

        UpdateCoinCount();

        Debug.Log("Coins Collected: " + coinsCollected);
    }

    void UpdateCoinCount()
    {
        if (coinCountText != null)
            coinCountText.text = "Coins: " + coinsCollected;
    }

    public void ShowFinalScore()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Your Score: " + currentScore;

        if (finalDistanceText != null)
            finalDistanceText.text = "Distance: " + currentDistance + " m";
    }
}
