using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Assign the obstacle prefab
    public float spawnInterval = 1f; // Time between spawns
    [SerializeField] float minYBuffer = 1f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning

    private Camera mainCamera;
    private float spawnTimer = 0f;

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
        float spawnY = Random.Range(
            mainCamera.transform.position.y - cameraHeight / 2 + minYBuffer,
            mainCamera.transform.position.y + cameraHeight / 2 - maxYBuffer
        );

        // Randomly decide whether to spawn on the left or right
        bool spawnOnLeft = Random.value > 0.5f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spawnX = spawnOnLeft
            ? mainCamera.transform.position.x - cameraWidth / 2 // Left border
            : mainCamera.transform.position.x + cameraWidth / 2; // Right border

        // Instantiate the obstacle
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // Set the launch direction
        BirdObstacle behavior = obstacle.GetComponent<BirdObstacle>();
        if (behavior != null)
        {
            behavior.SetLaunchDirection(spawnOnLeft);
        }
    }
}
