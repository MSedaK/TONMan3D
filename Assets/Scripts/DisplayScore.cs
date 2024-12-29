using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;
    private int lastDisplayedScore = -1;
    private float updateCheckInterval = 0.1f;
    private float nextUpdateCheck = 0f;

    void Start()
    {
        _ = GameManager.Instance;
        InitializeScoreDisplay();
    }

    void Update()
    {
        if (Time.time >= nextUpdateCheck)
        {
            nextUpdateCheck = Time.time + updateCheckInterval;
            UpdateScoreIfNeeded();
        }
    }

    private void InitializeScoreDisplay()
    {
        if (scoreText == null)
        {
            scoreText = GetComponent<Text>();
        }

        if (scoreText != null && GameManager.Instance != null)
        {
            UpdateScoreIfNeeded();
            Debug.Log("Score display initialized successfully");
        }
        else
        {
            Debug.LogError("Failed to initialize score display!");
        }
    }

    private void UpdateScoreIfNeeded()
    {
        if (scoreText != null && GameManager.Instance != null)
        {
            int currentScore = GameManager.GetCurrentScore();
            if (currentScore != lastDisplayedScore)
            {
                lastDisplayedScore = currentScore;
                scoreText.text = $"Score: {currentScore}";
                Debug.Log($"Score display updated to: {currentScore}");
            }
        }
    }
}
