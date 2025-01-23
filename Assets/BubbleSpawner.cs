using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign the bubble prefab
    public float spawnInterval = 1f; // Time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn bubbles
    [SerializeField] float minXBuffer = 1f; // Minimum buffer for X spawning
    [SerializeField] float maxXBuffer = 1f; // Maximum buffer for X spawning
    [SerializeField] float minYBuffer = 2f; // Minimum buffer for Y spawning
    [SerializeField] float maxYBuffer = 4f; // Maximum buffer for Y spawning

    private Camera mainCamera;

    void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;

        // Start spawning bubbles
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
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

        // Instantiate the bubble
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }
}
