using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign the bubble prefab
    public float spawnInterval = 1f; // Time between spawns
    public float spawnOffset = 2f; // How far above the camera to spawn bubbles
    public float bubbleDownForce = -2f; // Downward force applied to bubbles
    [SerializeField] float minX;
    [SerializeField] float maxX;
    public float spawnXBuffer = 1f; // Extra horizontal buffer for spawning bubbles

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
        float randomxBuffer = Random.Range(minX, maxX);
        // Randomize the X position within the camera's horizontal bounds plus buffer
        float randomX = Random.Range(mainCamera.transform.position.x - cameraWidth / 2 - randomxBuffer,
                                     mainCamera.transform.position.x + cameraWidth / 2 + randomxBuffer);

        // Spawn slightly above the camera's view
        float spawnY = mainCamera.transform.position.y + (cameraHeight / 2) + spawnOffset;

        // Instantiate the bubble
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // Apply downward force to the bubble
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0f, bubbleDownForce);
        }
    }
}
