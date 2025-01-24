using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin; // Assign the coin prefab
    public float spawnInterval = 1f; // Initial time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn coins
    [SerializeField] float minXBuffer = 1f; // Minimum buffer for X spawning
    [SerializeField] float maxXBuffer = 1f; // Maximum buffer for X spawning
    [SerializeField] float minYBuffer = 2f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning
    [SerializeField] float minSpawnDistance = 1f; // Minimum distance between entities

    private Camera mainCamera;
    private float elapsedTime = 0f; // Track elapsed time

    // Shared list to track all active entity positions
   

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;

        // Start spawning coins
        InvokeRepeating(nameof(SpawnCoins), 0f, spawnInterval);
    }

    void Update()
    {
        // Increase spawn rate every 20 seconds
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 20f)
        {
            elapsedTime = 0f; // Reset timer
            spawnInterval *= 0.95f; // Increase spawn rate by reducing interval by 5%

            // Update the spawn rate with the new interval
            CancelInvoke(nameof(SpawnCoins));
            InvokeRepeating(nameof(SpawnCoins), 0f, spawnInterval);
        }
    }

    void SpawnCoins()
    {
        // Calculate camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Randomize the X and Y buffer values
        float randomXBuffer = Random.Range(minXBuffer, maxXBuffer);
        float randomYBuffer = Random.Range(minYBuffer, maxYBuffer);

        // Find a valid spawn position
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

        // Spawn the coin if a valid position is found
        if (attempts < 10)
        {
          GameObject game =   Instantiate(coin, spawnPosition, Quaternion.identity);
            GameManager.instance.activeEntity.Add(game);
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

    void FixedUpdate()
    {
        // Clean up active entity positions (optional, if coins are destroyed or move out of bounds)
       
    }
}
