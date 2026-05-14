using UnityEngine;
using TMPro; // Needed for the Score HUD
using UnityEngine.SceneManagement; // Needed for Restart logic

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig config;
    [SerializeField] private TextMeshProUGUI scoreText; // Assign this in Inspector [cite: 20]

    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }

    // Core Requirement 1: IsGameOver flag 
    public bool IsGameOver { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ScrollSpeed = config.startSpeed;
    }

    void Update()
    {
        // Core Requirement 2: Restart Logic 
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        ScrollSpeed = Mathf.Min(ScrollSpeed + config.speedIncreaseRate * Time.deltaTime, config.maxSpeed);
        Distance += ScrollSpeed * Time.deltaTime;

        // Core Requirement 3: Score HUD 
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (int)Distance;
        }
    }

    public void EndGame()
    {
        IsGameOver = true;
        Debug.Log("Game Over! Final Score: " + (int)Distance);
        // Tip: You can toggle a "Game Over" UI panel here later.
    }
}