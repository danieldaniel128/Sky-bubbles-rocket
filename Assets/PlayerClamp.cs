using UnityEngine;

public class PlayerClamp : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera
    public float xBuffer = 0.5f; // Optional buffer for X axis
    public float yBuffer = 0.5f; // Optional buffer for Y axis
    [SerializeField] private AudioClip audioSource;

    private void OnEnable()
    {
        SoundManager.Instance.PlayBGM(audioSource);
    }
    private void Start()
    {
        SoundManager.Instance.PlayBGM(audioSource);
    }
    void Update()
    {
        ClampPlayerPosition();
       
    }
    private void OnDisable()
    {
        SoundManager.Instance.StopBGM();
    }
    private void ClampPlayerPosition()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Auto-assign the main camera if not set
        }

        // Get the camera's world boundaries
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the min and max bounds for clamping
        float minX = mainCamera.transform.position.x - (cameraWidth / 2) + xBuffer;
        float maxX = mainCamera.transform.position.x + (cameraWidth / 2) - xBuffer;
        float minY = mainCamera.transform.position.y - (cameraHeight / 2) + yBuffer;
        float maxY = mainCamera.transform.position.y + (cameraHeight / 2) - yBuffer;

        // Get the player's current position
        Vector3 clampedPosition = transform.position;

        // Clamp the player's position within the calculated bounds
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        // Apply the clamped position to the player
        transform.position = clampedPosition;
    }
}
