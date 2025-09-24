using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int scoreValue = 10;
    public CollectibleType type = CollectibleType.Coin;
    
    [Header("Animation")]
    public float rotationSpeed = 90f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    
    [Header("Effects")]
    public GameObject collectEffect;
    public AudioClip collectSound;
    
    private Vector3 startPosition;
    
    public enum CollectibleType
    {
        Coin,
        PowerUp,
        HealthPack
    }
    
    private void Start()
    {
        startPosition = transform.position;
    }
    
    private void Update()
    {
        // Rotate the collectible
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        
        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }
    
    private void CollectItem()
    {
        // Add score based on type
        switch (type)
        {
            case CollectibleType.Coin:
                GameManager.Instance.AddScore(scoreValue);
                break;
            case CollectibleType.PowerUp:
                GameManager.Instance.AddScore(scoreValue);
                // Add power-up effect here
                break;
            case CollectibleType.HealthPack:
                // Restore health/lives
                break;
        }
        
        // Play sound effect
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        
        // Spawn collection effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        
        // Destroy the collectible
        Destroy(gameObject);
    }
}