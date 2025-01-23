using UnityEngine;


using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.5f; // Duration of the shake
    public float magnitude = 0.2f; // Magnitude of the shake

    private Vector3 originalPosition; // Original camera position
    private bool isShaking = false;

    void Start()
    {
        // Save the original position of the camera
        originalPosition = transform.localPosition;
    }

    public void StartShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }
    
    private IEnumerator Shake()
    {
        isShaking = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Random offset
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera to its original position
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
