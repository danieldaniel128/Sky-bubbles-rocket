//using UnityEngine;
//using System.Collections.Generic;

//public class BorderObstacleSpawner : MonoBehaviour
//{
//    public GameObject obstacle; // Assign the obstacle prefab
//    [SerializeField]BirdObstacle birdObstacle; // Reference to the BirdObstacle script
//    public float spawnInterval = 1f; // Time between spawns
//    [SerializeField] float minYBuffer = 1f; // Minimum buffer for Y spawning
//    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning
//    [SerializeField] float minSpawnDistance = 5f; // Minimum distance between obstacles
//    private Camera mainCamera;
//    private float spawnTimer = 0f; // Timer to track spawn intervals
//    private List<Vector3> activeObstaclePositions = new List<Vector3>(); // Track obstacle positions

//    void Start()
//    {
//        // Get reference to the main camera
//        mainCamera = Camera.main;
//    }

//    void Update()
//    {
//        // Update timer
//        spawnTimer += Time.deltaTime;

//        // Check if it's time to spawn an obstacle
//        if (spawnTimer >= spawnInterval)
//        {
//            SpawnObstacle();
//            spawnTimer = 0f; // Reset spawn timer
//        }
//    }

//    void SpawnObstacle()
//    {
//        // Calculate camera bounds
//        float cameraHeight = 2f * mainCamera.orthographicSize;

//        // Randomize the Y position within the camera's vertical bounds
//        float spawnY;
//        int attempts = 0;
//        do
//        {
//            spawnY = Random.Range(
//                mainCamera.transform.position.y - cameraHeight / 2 + minYBuffer,
//                mainCamera.transform.position.y + cameraHeight / 2 - maxYBuffer
//            );
//            attempts++;
//        } while (IsPositionTooClose(new Vector3(0, spawnY, 0)) && attempts < 10);

//        // Dynamically switch between left or right border
//        bool spawnOnLeft = Random.value > 0.5f; // Randomly choose left or right
//        float cameraWidth = cameraHeight * mainCamera.aspect;
//        float spawnX = spawnOnLeft
//            ? mainCamera.transform.position.x - cameraWidth / 2 // Left border
//            : mainCamera.transform.position.x + cameraWidth / 2; // Right border

//        // Spawn the obstacle if a valid position is found
//        if (attempts < 10)
//        {
//            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
//            BirdObstacle newObstacle = Instantiate(birdObstacle, spawnPosition, Quaternion.identity);
//            newObstacle.SetLaunchDirection(spawnOnLeft); // Set the launch direction
//            activeObstaclePositions.Add(spawnPosition); // Track the position
//        }
//    }

//    bool IsPositionTooClose(Vector3 position)
//    {
//        // Check if the position is too close to existing obstacles
//        foreach (Vector3 activePosition in activeObstaclePositions)
//        {
//            if (Vector3.Distance(position, activePosition) < minSpawnDistance)
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    void FixedUpdate()
//    {
//        // Remove obstacles that move off-screen from the tracking list
//        activeObstaclePositions.RemoveAll(pos => Mathf.Abs(pos.y) > mainCamera.transform.position.y + 10f);
//    }
//}
