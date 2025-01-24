using UnityEngine;

public class BirdObstacle : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Vector2 direction;
    private Camera mainCamera;

    public void SetLaunchDirection(bool fromLeft)
    {
        // Randomize the angle within a 180-degree range
        float angle = Random.Range(-45f, 45f);

        // Determine the launch direction
        if (fromLeft)
        {
            direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // Launch to the right
        }
        else
        {
            direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // Launch to the left
        }
    }

    void Start()
    {
        // Reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Move the obstacle in the set direction
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Check if the obstacle has moved off-screen
        CheckOffScreen();
    }

    private void CheckOffScreen()
    {
        // Calculate camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Destroy the obstacle if it moves off the screen
        if (transform.position.x > mainCamera.transform.position.x + cameraWidth / 2 ||
            transform.position.x < mainCamera.transform.position.x - cameraWidth / 2)
        {
            Destroy(gameObject);
        }
    }
}
