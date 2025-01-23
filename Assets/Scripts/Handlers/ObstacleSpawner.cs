using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle; // Assign the obstacle prefab
    public float spawnInterval = 1f; // Initial time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn obstacles
    [SerializeField] float minXBuffer = 1f; // Minimum buffer for X spawning
    [SerializeField] float maxXBuffer = 1f; // Maximum buffer for X spawning
    [SerializeField] float minYBuffer = 2f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning

    private Camera mainCamera;
    private float elapsedTime = 0f; // Track elapsed time

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;

        // Start spawning obstacles
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
    }

    void Update()
    {
        // Increase spawn rate every 10 seconds
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 10f)
        {
            elapsedTime = 0f; // Reset timer
            spawnInterval *= 0.8f; // Increase spawn rate by reducing interval by 20%

            // Update the spawn rate with the new interval
            CancelInvoke(nameof(SpawnObstacle));
            InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
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

        // Clamp the X position within the camera's horizontal bounds
        float spawnX = Random.Range(
            mainCamera.transform.position.x - cameraWidth / 2 + randomXBuffer,
            mainCamera.transform.position.x + cameraWidth / 2 - randomXBuffer
        );

        // Spawn slightly above the camera's view
        float spawnY = mainCamera.transform.position.y + (cameraHeight / 2) + randomYBuffer;

        // Instantiate the obstacle
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
