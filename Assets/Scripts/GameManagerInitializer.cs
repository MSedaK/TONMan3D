using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    void Awake()
    {
        // Ensure GameManager exists
        _ = GameManager.Instance;
    }
}