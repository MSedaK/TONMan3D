using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int Score;
    public Text txt;
    string scoreT, ScoreT1, ScoreDisplay;


    // Start is called before the first frame update
    void Start()
    {
        Score = 0; //it resets the score to 0 when the game stars.

    }
    // Update is called once per frame
    void Update()
    {
        ScoreT1 = Score.ToString();
        scoreT = "Score: " + ScoreT1;
        txt.text = scoreT;
        PlayerPrefs.SetInt(ScoreDisplay, Score); //its sets the score to the lates update.

        if (Score == 32)
        {
            SceneManager.LoadScene("Level2");
        }
        if (Score == 31)
        {
            SceneManager.LoadScene("Level3");
        }
        if (Score == 36)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public static void incrementScore()
    {
        Score++;
    }
}