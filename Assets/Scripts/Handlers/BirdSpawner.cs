using UnityEngine;
using System.Collections.Generic;

public class BirdSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Assign the obstacle prefab
    public float spawnInterval = 1f; // Time between spawns
    [SerializeField] float minYBuffer = 1f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning
    [SerializeField] float minSpawnDistance = 2f; // Minimum distance between obstacles
    [SerializeField] List<AudioClip> birdSoundsLeftToRight;
    [SerializeField] AudioClip firstRightToLeft;
    private Camera mainCamera;
    private float spawnTimer = 0f;

    // Track active obstacles to prevent overlapping
    

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn an obstacle
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            spawnTimer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;

        // Randomize Y position within the camera's vertical bounds
        float spawnY;
        int attempts = 0;
        bool validPosition = false;

        do
        {
            spawnY = Random.Range(
                mainCamera.transform.position.y - cameraHeight / 2 + minYBuffer,
                mainCamera.transform.position.y + cameraHeight / 2 - maxYBuffer
            );

            // Check if the position is valid
            validPosition = IsPositionValid(new Vector3(0, spawnY, 0));
            attempts++;
        }
        while (!validPosition && attempts < 10);

        if (!validPosition)
        {
            Debug.LogWarning("Could not find a valid position for spawning after 10 attempts.");
            return;
        }

        // Randomly decide whether to spawn on the left or right
        bool spawnOnLeft = Random.value > 0.5f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spawnX = spawnOnLeft
            ? mainCamera.transform.position.x - cameraWidth / 2 // Left border
            : mainCamera.transform.position.x + cameraWidth / 2; // Right border

        // Instantiate the obstacle
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, transform);

        // Add the obstacle to the active list
        GameManager.instance.activeBird.Add(obstacle);

        // Set the launch direction
        BirdObstacle behavior = obstacle.GetComponent<BirdObstacle>();
        if (behavior != null)
        {
            AudioClip clip;
            if (spawnOnLeft) {
                 clip = birdSoundsLeftToRight[Random.Range(0, birdSoundsLeftToRight.Count)];
            }
            else
            {
                clip = firstRightToLeft;
            }
            behavior.SetLaunchDirection(spawnOnLeft,clip);
        }
    }


    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject obstacle in GameManager.instance.activeBird)
        {
            if (obstacle == null) continue;

            float distance = Vector3.Distance(obstacle.transform.position, position);
            if (distance < minSpawnDistance)
            {
                return false;
            }
        }
        return true;
    }
}
