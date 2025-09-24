using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game Settings")]
    public int score = 0;
    public int lives = 3;
    public bool gameOver = false;
    public bool gamePaused = false;
    
    [Header("UI References")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text livesText;
    
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip gameOverSound;
    public AudioClip collectSound;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        UpdateUI();
        Time.timeScale = 1f;
    }
    
    private void Update()
    {
        // Handle pause with Android back button or escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    
    public void AddScore(int points)
    {
        if (!gameOver)
        {
            score += points;
            UpdateUI();
            
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
            }
        }
    }
    
    public void LoseLife()
    {
        if (!gameOver)
        {
            lives--;
            UpdateUI();
            
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }
    
    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
            
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }
        
        // Save high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
    
    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        
        if (pausePanel != null)
            pausePanel.SetActive(true);
            
        if (musicSource != null)
            musicSource.Pause();
    }
    
    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        
        if (pausePanel != null)
            pausePanel.SetActive(false);
            
        if (musicSource != null)
            musicSource.UnPause();
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
            
        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}