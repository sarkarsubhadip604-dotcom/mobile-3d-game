using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rotationSpeed = 720f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Mobile Controls")]
    public FixedJoystick joystick;
    public UnityEngine.UI.Button jumpButton;
    
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;
    private Animator animator;
    
    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        // Setup mobile controls
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }
    }
    
    private void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Get input
        GetInput();
        
        // Handle animations
        UpdateAnimations();
    }
    
    private void FixedUpdate()
    {
        // Apply movement
        Move();
    }
    
    private void GetInput()
    {
        // Mobile input via joystick
        if (joystick != null)
        {
            moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        else
        {
            // Fallback to keyboard input for testing
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            moveDirection = new Vector3(horizontal, 0, vertical);
        }
        
        // Jump input (for keyboard testing)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    
    private void Move()
    {
        // Apply movement
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
        
        // Rotate player to face movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 
                                                         rotationSpeed * Time.fixedDeltaTime);
        }
    }
    
    public void Jump()
    {
        if (isGrounded && !GameManager.Instance.gameOver && !GameManager.Instance.gamePaused)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
    
    private void UpdateAnimations()
    {
        if (animator != null)
        {
            float speed = moveDirection.magnitude;
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsGrounded", isGrounded);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && landSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(landSound);
        }
        
        // Handle obstacle collision
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.LoseLife();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Handle collectibles
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(10);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PowerUp"))
        {
            GameManager.Instance.AddScore(50);
            // Add power-up effect here
            Destroy(other.gameObject);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}