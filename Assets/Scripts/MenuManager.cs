using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    
    [Header("UI Text")]
    public UnityEngine.UI.Text highScoreText;
    
    [Header("Audio")]
    public AudioSource backgroundMusic;
    
    private void Start()
    {
        // Show main menu panel
        ShowMainMenu();
        
        // Display high score
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        
        // Start background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void ShowMainMenu()
    {
        mainMenuPanel?.SetActive(true);
        settingsPanel?.SetActive(false);
        creditsPanel?.SetActive(false);
    }
    
    public void ShowSettings()
    {
        mainMenuPanel?.SetActive(false);
        settingsPanel?.SetActive(true);
        creditsPanel?.SetActive(false);
    }
    
    public void ShowCredits()
    {
        mainMenuPanel?.SetActive(false);
        settingsPanel?.SetActive(false);
        creditsPanel?.SetActive(true);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: 0";
        }
    }
}