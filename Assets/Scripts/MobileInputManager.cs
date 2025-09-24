using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    [Header("UI Elements")]
    public FixedJoystick movementJoystick;
    public UnityEngine.UI.Button jumpButton;
    public UnityEngine.UI.Button pauseButton;
    
    [Header("Settings")]
    public bool showControlsOnDesktop = true;
    
    private void Start()
    {
        SetupMobileUI();
    }
    
    private void SetupMobileUI()
    {
        // Show/hide controls based on platform
        bool isMobile = Application.isMobilePlatform;
        bool showControls = isMobile || showControlsOnDesktop;
        
        if (movementJoystick != null)
            movementJoystick.gameObject.SetActive(showControls);
            
        if (jumpButton != null)
            jumpButton.gameObject.SetActive(showControls);
            
        // Setup button events
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(() => {
                if (GameManager.Instance.gamePaused)
                    GameManager.Instance.ResumeGame();
                else
                    GameManager.Instance.PauseGame();
            });
        }
    }
    
    public Vector2 GetMovementInput()
    {
        if (movementJoystick != null)
        {
            return new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        }
        return Vector2.zero;
    }
}