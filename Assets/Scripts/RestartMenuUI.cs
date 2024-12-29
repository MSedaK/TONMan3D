using UnityEngine;
using UnityEngine.UI;

public class RestartMenuUI : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        if (finalScoreText != null)
        {
            int lastScore = GameManager.GetLastScore();
            finalScoreText.text = $"Final Score: {lastScore}";
            Debug.Log($"Displaying final score: {lastScore}");
        }
    }
}