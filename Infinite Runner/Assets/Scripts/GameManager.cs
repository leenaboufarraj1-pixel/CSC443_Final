using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Using the New Input System as seen in your screenshots

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private GameConfig config;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;      // The HUD during play
    [SerializeField] private GameObject gameOverPanel;      // The Panel that appears on death
    [SerializeField] private TextMeshProUGUI finalScoreText; // Score text on the Panel

    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }
    public bool IsGameOver { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        Time.timeScale = 1f; // Ensure game is unpaused on start
        ScrollSpeed = config.startSpeed;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (IsGameOver) return;

        ScrollSpeed = Mathf.Min(ScrollSpeed + config.speedIncreaseRate * Time.deltaTime, config.maxSpeed);
        Distance += ScrollSpeed * Time.deltaTime;

        if (scoreText != null) scoreText.text = "Distance: " + (int)Distance;
    }

    public void EndGame()
    {
        IsGameOver = true;
        Time.timeScale = 0f; // This stops all physics and movement visually 

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null) finalScoreText.text = "Final Score: " + (int)Distance;
        }
    }

    // This function will be linked to your UI Button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}