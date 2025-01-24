using System.Collections.Generic;
using UnityEngine;

public class BirdObstacle : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Vector2 direction;
    private Camera mainCamera;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> SpriteOptions;

    private bool isLaunched = false; // Tracks if the obstacle has been launched

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
           spriteRenderer.flipX = true;
            direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // Launch to the left
        }

        // Draw the trajectory immediately
    }

    void Start()
    {
       spriteRenderer.sprite = SpriteOptions[Random.Range(0, SpriteOptions.Count)];
        // Reference to the main camera
        mainCamera = Camera.main;

        // Initialize LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;

        // Start a delay before launching
        Invoke(nameof(Launch), 3f);
    }

    void Update()
    {
        DrawTrajectory();

        if (isLaunched)
        {
            // Move the obstacle in the set direction
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Check if the obstacle has moved off-screen
            CheckOffScreen();
        }
    }

    private void Launch()
    {
        isLaunched = true; // Enable movement
        lineRenderer.enabled = false; // Disable trajectory line once the obstacle starts moving
    }

    private void DrawTrajectory()
    {
        if (lineRenderer == null) return;

        // Set the starting point of the line to the obstacle's position
        Vector3 startPoint = transform.position;

        // Calculate the endpoint based on the direction and screen size
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 endPoint = startPoint + (Vector3)(direction.normalized * cameraWidth * 2f); // Extend the line off-screen

        // Set the LineRenderer positions
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
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
    private void OnDestroy()
    {
        GameManager.instance.activeBird.Remove(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the obstacle when it collides with the player
        if (collision.attachedRigidbody.gameObject.CompareTag("Rocket"))
        {
            if (collision.attachedRigidbody.TryGetComponent(out PlayerController player))
            {
                player.LoseLives();
                Destroy(gameObject);

            }
        }
    }
}

