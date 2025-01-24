using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]float downwardForce = 1f;
    [SerializeField] int minSpeed;
    [SerializeField] int maxSpeed;
    [SerializeField] AudioClip popSound;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        downwardForce = Random.Range(minSpeed, maxSpeed+1);
        mainCamera = Camera.main; // Reference to the main camera
                                  //downwardForce *= 20;

    }
    private void Update()
    {
        // Calculate camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float minY = mainCamera.transform.position.y - cameraHeight / 2;

        // Destroy the bubble if it moves below the bottom of the camera
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movement = new Vector2(0, -downwardForce * Time.deltaTime);
        transform.position += movement;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.attachedRigidbody.gameObject.CompareTag("Rocket"))
            {
                if (collision.attachedRigidbody.TryGetComponent(out PlayerController player))
                {
                   player.ReFuel(10);
                    SoundManager.Instance.PlaySFX(popSound);    
                    Destroy(gameObject);

                }
            }
        }
    }
    private void OnDestroy()
    {
        GameManager.instance.activeEntity.Remove(transform.gameObject);


    }
}
