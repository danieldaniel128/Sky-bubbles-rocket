using UnityEngine;
using System.Collections.Generic;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign the bubble prefab
    public float spawnInterval = 0.90f; // Time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn bubbles
    [SerializeField] float minXBuffer = 1f; // Minimum buffer for X spawning
    [SerializeField] float maxXBuffer = 1f; // Maximum buffer for X spawning
    [SerializeField] float minYBuffer = 2f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning
    [SerializeField] float minSpawnDistance = 1f; // Minimum distance between entities

    private Camera mainCamera;
    private float spawnTimer = 0f; // Timer to track spawn intervals
   

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Update timers
        spawnTimer += Time.deltaTime;
        

        // Check if it's time to spawn a bubble
        if (spawnTimer >= spawnInterval)
        {
            SpawnBubble();
            spawnTimer = 0f; // Reset spawn timer
        }

        // Increase spawn rate every 20 seconds
       
    }

    public void SetSpawnInterval()
    {
        spawnInterval *= 0.95f;
        spawnInterval = Mathf.Max(0.1f, spawnInterval);
    }

    void SpawnBubble()
    {
        // Calculate camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Randomize the X and Y buffer values
        float randomXBuffer = Random.Range(minXBuffer, maxXBuffer);
        float randomYBuffer = Random.Range(minYBuffer, maxYBuffer);

        // Try to find a valid spawn position
        Vector3 spawnPosition;
        int attempts = 0;
        do
        {
            // Randomize spawn position within camera bounds
            float spawnX = Random.Range(
                mainCamera.transform.position.x - cameraWidth / 2 + randomXBuffer,
                mainCamera.transform.position.x + cameraWidth / 2 - randomXBuffer
            );

            float spawnY = mainCamera.transform.position.y + (cameraHeight / 2) + randomYBuffer;
            spawnPosition = new Vector3(spawnX, spawnY, 0f);

            attempts++;
        }
        while (IsPositionOverlapping(spawnPosition) && attempts < 10);

        // Spawn the bubble if a valid position is found
        if (attempts < 10)
        {
            GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
            GameManager.instance.activeEntity.Add(bubble);
        }
    }

    bool IsPositionOverlapping(Vector3 position)
    {
        // Check if the position is too close to existing entity positions
        foreach (GameObject activePosition in GameManager.instance.activeEntity)
        {
            if (Vector3.Distance(position, activePosition.transform.position) < minSpawnDistance)
            {
                return true;
            }
        }
        return false;
    }
}
