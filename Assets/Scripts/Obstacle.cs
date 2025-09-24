using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public int damage = 1;
    public bool destroyOnContact = false;
    
    [Header("Movement")]
    public bool moveObstacle = false;
    public Vector3 moveDirection = Vector3.forward;
    public float moveSpeed = 2f;
    public float moveDistance = 5f;
    
    [Header("Effects")]
    public GameObject hitEffect;
    public AudioClip hitSound;
    
    private Vector3 startPosition;
    private bool movingForward = true;
    
    private void Start()
    {
        startPosition = transform.position;
    }
    
    private void Update()
    {
        if (moveObstacle)
        {
            MoveObstacle();
        }
    }
    
    private void MoveObstacle()
    {
        // Move the obstacle back and forth
        if (movingForward)
        {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
            
            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= moveDirection.normalized * moveSpeed * Time.deltaTime;
            
            if (Vector3.Distance(startPosition, transform.position) <= 0.1f)
            {
                movingForward = true;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerHit();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerHit();
        }
    }
    
    private void HandlePlayerHit()
    {
        // Deal damage to player
        for (int i = 0; i < damage; i++)
        {
            GameManager.Instance.LoseLife();
        }
        
        // Play hit sound
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        
        // Spawn hit effect
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        
        // Destroy obstacle if set to
        if (destroyOnContact)
        {
            Destroy(gameObject);
        }
    }
}