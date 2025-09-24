using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public List<GameObject> levelPrefabs = new List<GameObject>();
    public Transform spawnPoint;
    public float levelLength = 50f;
    
    [Header("Generation Settings")]
    public int levelsToKeepActive = 3;
    public float despawnDistance = 100f;
    
    private Queue<GameObject> activeLevels = new Queue<GameObject>();
    private Transform player;
    private int currentLevelIndex = 0;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure player has 'Player' tag.");
        }
        
        // Generate initial levels
        for (int i = 0; i < levelsToKeepActive; i++)
        {
            GenerateLevel();
        }
    }
    
    private void Update()
    {
        if (player != null)
        {
            ManageLevels();
        }
    }
    
    private void ManageLevels()
    {
        // Check if we need to generate a new level
        if (activeLevels.Count > 0)
        {
            GameObject lastLevel = GetLastLevel();
            float distanceToLastLevel = Vector3.Distance(player.position, lastLevel.transform.position);
            
            if (distanceToLastLevel < levelLength * 2f)
            {
                GenerateLevel();
            }
        }
        
        // Remove old levels that are too far behind
        if (activeLevels.Count > levelsToKeepActive)
        {
            GameObject oldestLevel = activeLevels.Peek();
            float distanceToOldest = Vector3.Distance(player.position, oldestLevel.transform.position);
            
            if (distanceToOldest > despawnDistance)
            {
                RemoveOldestLevel();
            }
        }
    }
    
    private void GenerateLevel()
    {
        if (levelPrefabs.Count == 0)
        {
            Debug.LogWarning("No level prefabs assigned!");
            return;
        }
        
        // Choose a random level prefab
        int randomIndex = Random.Range(0, levelPrefabs.Count);
        GameObject levelPrefab = levelPrefabs[randomIndex];
        
        // Calculate spawn position
        Vector3 spawnPosition = spawnPoint.position + Vector3.forward * (currentLevelIndex * levelLength);
        
        // Instantiate the level
        GameObject newLevel = Instantiate(levelPrefab, spawnPosition, Quaternion.identity);
        newLevel.name = $"Level_{currentLevelIndex}";
        
        // Add to active levels
        activeLevels.Enqueue(newLevel);
        currentLevelIndex++;
        
        Debug.Log($"Generated level {currentLevelIndex} at position {spawnPosition}");
    }
    
    private void RemoveOldestLevel()
    {
        if (activeLevels.Count > 0)
        {
            GameObject oldLevel = activeLevels.Dequeue();
            Debug.Log($"Removing old level: {oldLevel.name}");
            Destroy(oldLevel);
        }
    }
    
    private GameObject GetLastLevel()
    {
        GameObject lastLevel = null;
        foreach (GameObject level in activeLevels)
        {
            lastLevel = level;
        }
        return lastLevel;
    }
    
    public void ResetLevels()
    {
        // Clear all active levels
        while (activeLevels.Count > 0)
        {
            GameObject level = activeLevels.Dequeue();
            if (level != null)
                Destroy(level);
        }
        
        currentLevelIndex = 0;
        
        // Regenerate initial levels
        for (int i = 0; i < levelsToKeepActive; i++)
        {
            GenerateLevel();
        }
    }
}