using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle; // Assign the obstacle prefab
    public float spawnInterval = 1f; // Initial time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn obstacles
    [SerializeField] float minXBuffer = 1f; // Minimum buffer for X spawning
    [SerializeField] float maxXBuffer = 1f; // Maximum buffer for X spawning
    [SerializeField] float minYBuffer = 2f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning
    [SerializeField] float minSpawnDistance = 1f; // Minimum distance between entities

    private Camera mainCamera;
    private float spawnTimer = 0f; // Timer to track spawn intervals
    private float spawnRateIncreaseTimer = 0f; // Timer to track spawn rate increase intervals

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Update timers
        spawnTimer += Time.deltaTime;
        spawnRateIncreaseTimer += Time.deltaTime;

        // Check if it's time to spawn an obstacle
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            spawnTimer = 0f; // Reset spawn timer
        }

        // Increase spawn rate every 10 seconds
        if (spawnRateIncreaseTimer >= 10f)
        {
            spawnRateIncreaseTimer = 0f; // Reset rate increase timer
            spawnInterval *= 0.9f; // Reduce interval by 10% to increase spawn rate
            spawnInterval = Mathf.Max(0.1f, spawnInterval); // Prevent spawnInterval from getting too small
        }
    }

    void SpawnObstacle()
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

        // Spawn the obstacle if a valid position is found
        if (attempts < 10)
        {
            GameObject game = Instantiate(obstacle, spawnPosition, Quaternion.identity);
            GameManager.instance.activeEntity.Add(game);
        }
    }

    bool IsPositionOverlapping(Vector3 position)
    {
        // Check if the position is too close to any existing entity positions
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
