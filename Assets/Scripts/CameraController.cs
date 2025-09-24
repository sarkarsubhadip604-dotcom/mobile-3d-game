using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    
    [Header("Camera Settings")]
    public float followSpeed = 2f;
    public float rotationSpeed = 2f;
    public bool enableMobileInput = true;
    
    [Header("Touch Controls")]
    public float touchSensitivity = 2f;
    public float minYAngle = -30f;
    public float maxYAngle = 60f;
    
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    
    private void Start()
    {
        if (target == null && GameObject.FindWithTag("Player"))
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        
        currentRotation = transform.eulerAngles;
    }
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        HandleInput();
        UpdateCameraPosition();
    }
    
    private void HandleInput()
    {
        if (GameManager.Instance.gamePaused) return;
        
        if (enableMobileInput && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;
                
                currentRotation.x -= deltaPosition.y * touchSensitivity * Time.deltaTime;
                currentRotation.y += deltaPosition.x * touchSensitivity * Time.deltaTime;
                
                currentRotation.x = Mathf.Clamp(currentRotation.x, minYAngle, maxYAngle);
            }
        }
        
        // Mouse input for testing in editor
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            currentRotation.x -= mouseY * rotationSpeed;
            currentRotation.y += mouseX * rotationSpeed;
            
            currentRotation.x = Mathf.Clamp(currentRotation.x, minYAngle, maxYAngle);
        }
    }
    
    private void UpdateCameraPosition()
    {
        // Apply rotation
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        
        // Calculate desired position
        Vector3 desiredPosition = target.position + rotation * offset;
        
        // Smooth camera movement
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, 
                                              ref smoothVelocity, 1f / followSpeed);
        
        // Look at target
        transform.LookAt(target.position + Vector3.up);
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}