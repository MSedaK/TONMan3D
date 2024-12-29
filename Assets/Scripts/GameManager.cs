using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
<<<<<<< Updated upstream
    private static GameManager instance;
    public static GameManager Instance { get; private set; }

    private static int currentScore;
    private static int lastScore;
    public Text scoreText;
    private int currentLevelCoins;
    private bool isTransitioning = false;
    private bool isUIInitialized = false;
    private bool isCheckingTransition = false;

    private readonly Dictionary<string, LevelData> levelRequirements = new Dictionary<string, LevelData>()
=======
    static int Score;
    public Text txt;

    void Start()
    {
        Score = PlayerPrefs.GetInt("Score", 0); // Ensure score persists across levels
        UpdateScoreUI();
    }

    void Update()
    {
        UpdateScoreUI();

        // Ensure level progression works without overriding logic
        if (Score == 20)
        {
            SceneManager.LoadScene("Level2");
        }
        if (Score == 35)
        {
            SceneManager.LoadScene("Level3");
        }
        if (Score == 60)
        {
            SceneManager.LoadScene("Level4");
        }
    }

    private void UpdateScoreUI()
    {
        if (txt != null)
        {
            txt.text = "Score: " + Score;
        }
    }

    public static void incrementScore()
>>>>>>> Stashed changes
    {
        {"Level1", new LevelData(33, "Level2", "First level")},
        {"Level2", new LevelData(41, "Level3", "Second level")},
        {"Level3", new LevelData(38, "MainMenu", "Final level")}
    };

    private class LevelData
    {
        public int RequiredCoins { get; private set; }
        public string NextLevel { get; private set; }
        public string LevelName { get; private set; }

        public LevelData(int coins, string next, string name)
        {
            RequiredCoins = coins;
            NextLevel = next;
            LevelName = name;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isTransitioning = false;
        isUIInitialized = false;

        // Reset level coins but keep total score
        currentLevelCoins = 0;

        StartCoroutine(InitializeUIWithRetry());

        if (levelRequirements.ContainsKey(scene.name))
        {
            var levelData = levelRequirements[scene.name];
            PlayerPrefs.SetString("LastLevel", scene.name);
            Debug.Log($"=== LEVEL LOADED ===\n" +
                     $"Scene: {levelData.LevelName}\n" +
                     $"Required Coins: {levelData.RequiredCoins}\n" +
                     $"Total Score: {currentScore}");
        }
    }

    private IEnumerator InitializeUIWithRetry()
    {
        int maxAttempts = 5;
        int attempts = 0;

        while (!isUIInitialized && attempts < maxAttempts)
        {
            yield return new WaitForSeconds(0.1f);

            scoreText = GameObject.FindGameObjectWithTag("ScoreText")?.GetComponent<Text>();

            if (scoreText != null)
            {
                isUIInitialized = true;
                UpdateScoreDisplay();
                Debug.Log($"UI initialized successfully. Current Score: {currentScore}");
            }
            else
            {
                attempts++;
                Debug.LogWarning($"Failed to find ScoreText. Attempt {attempts}/{maxAttempts}");
            }
        }

        if (!isUIInitialized)
        {
            Debug.LogError("Failed to initialize UI after maximum attempts!");
        }
    }

    void Update()
    {
        // Move periodic logging here, but keep completion check in IncrementScore
        if (!isTransitioning)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (levelRequirements.TryGetValue(currentScene, out LevelData levelData))
            {
                if (currentLevelCoins > 0 && currentLevelCoins % 5 == 0)
                {
                    Debug.Log($"Progress - {currentScene}: {currentLevelCoins}/{levelData.RequiredCoins} coins");
                }
            }
        }
    }

    private void CheckLevelCompletion()
    {
        if (isTransitioning) return;

        string currentScene = SceneManager.GetActiveScene().name;

        if (levelRequirements.TryGetValue(currentScene, out LevelData levelData))
        {
            Debug.Log($"Checking completion - Scene: {currentScene}, " +
                     $"Current Coins: {currentLevelCoins}, Required: {levelData.RequiredCoins}");

            if (currentLevelCoins == levelData.RequiredCoins)
            {
                isTransitioning = true;
                Debug.Log($"=== LEVEL COMPLETE ===\n" +
                         $"Scene: {currentScene}\n" +
                         $"Coins: {currentLevelCoins}/{levelData.RequiredCoins}\n" +
                         $"Next Level: {levelData.NextLevel}");

                StartCoroutine(TransitionToNextLevel(levelData.NextLevel));
            }
        }
    }

    private IEnumerator TransitionToNextLevel(string nextLevel)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log($"Loading next level: {nextLevel}");
        SceneManager.LoadScene(nextLevel);
    }

    public static void HandlePlayerDeath()
    {
        if (Instance != null)
        {
            lastScore = currentScore; // Store score before death
            string currentLevel = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LastLevel", currentLevel);

            Debug.Log($"Player died. Last Score: {lastScore}, Current Level: {currentLevel}");
            SceneManager.LoadScene("RestartMenu");
        }
    }

    public static int GetLastScore()
    {
        return lastScore;
    }

    public static void ResetScore()
    {
        if (Instance != null)
        {
            lastScore = currentScore; // Store last score before reset
            Debug.Log($"Resetting score. Previous: {currentScore}, Stored Last: {lastScore}");
            currentScore = 0;
            Instance.currentLevelCoins = 0;
            Instance.UpdateScoreDisplay();
        }
    }

    public static void IncrementScore()
    {
        if (Instance == null || Instance.isTransitioning) return;

        currentScore++;
        Instance.currentLevelCoins++;
        Instance.UpdateScoreDisplay();

        // Check level completion after each coin collection
        Instance.CheckLevelCompletion();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
            Debug.Log($"Score display updated: {currentScore}, UI Component: {(scoreText != null ? "Valid" : "Null")}");
        }
        else if (isUIInitialized)
        {
            Debug.LogError("Score Text reference lost! Attempting to reinitialize...");
            StartCoroutine(InitializeUIWithRetry());
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public static int GetCurrentScore()
    {
        return currentScore;
    }

    public static int GetLevelCoinsCount()
    {
        return Instance != null ? Instance.currentLevelCoins : 0;
    }
}