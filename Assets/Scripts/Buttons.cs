using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Code to assign functions to buttons to change scenes.
public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Buttonclicks;

    private void Start()
    {
        // Ensure GameManager exists
        _ = GameManager.Instance;
    }

    public void play()
    {
        Buttonclicks.Play();
        Invoke("play1", 0.2f);

    }

    public void goToMenu()
    {
        Buttonclicks.Play();
        Invoke("goToMenu1", 0.2f);
    }
    public void restartLevel()
    {
        Buttonclicks.Play();
        Invoke("restartLevel1", 0.2f);
    }
    public void quitGame()
    {
        Buttonclicks.Play();
        Invoke("quitGame1", 0.2f);
    }




    private void play1()
    {
        if (Buttonclicks != null)
        {
            Buttonclicks.Play();
        }

        GameManager.ResetScore();
        SceneManager.LoadScene("Level1");
    }

    private void goToMenu1()
    {
        if (Buttonclicks != null)
        {
            Buttonclicks.Play();
        }

        GameManager.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }
    private void restartLevel1()
    {
        if (Buttonclicks != null)
        {
            Buttonclicks.Play();
        }

        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        Debug.Log($"Restarting level: {lastLevel}");

        GameManager.ResetScore();
        SceneManager.LoadScene(lastLevel);
    }

    private void quitGame1()
    {
        Application.Quit();
    }


}
