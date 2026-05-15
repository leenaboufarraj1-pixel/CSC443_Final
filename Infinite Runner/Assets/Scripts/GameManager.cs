using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 
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

    [SerializeField] private GameObject pausePanel; 
    private bool _isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        Time.timeScale = 1f; 
        ScrollSpeed = config.startSpeed;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsGameOver && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isPaused) Resume();
            else Pause();
        }

        if (IsGameOver) return;

        ScrollSpeed = Mathf.Min(ScrollSpeed + config.speedIncreaseRate * Time.deltaTime, config.maxSpeed);
        Distance += ScrollSpeed * Time.deltaTime;

        if (scoreText != null) scoreText.text = "Distance: " + (int)Distance;
    }

    public void EndGame()
    {
        IsGameOver = true;
        Time.timeScale = 0f; 

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null) finalScoreText.text = "Final Score: " + (int)Distance;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Pause()
    {
        _isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Freezes the game visually 
    }

    public void Resume()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resumes the game
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // MUST reset time before leaving
        SceneManager.LoadScene(0); // Goes back to Main Menu
    }
}