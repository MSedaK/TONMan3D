using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;

        Debug.Log($"Level started with {totalCoins} coins placed in scene");

        // Verify coin count matches requirements
        if (totalCoins < GetRequiredCoins(currentScene))
        {
            Debug.LogError($"WARNING: Not enough coins in level! Placed: {totalCoins}, " +
                          $"Required: {GetRequiredCoins(currentScene)}");
        }
    }

    private int GetRequiredCoins(string sceneName)
    {
        return sceneName switch
        {
            "Level1" => 33,
            "Level2" => 41,
            "Level3" => 38,
            _ => 0
        };
    }
}