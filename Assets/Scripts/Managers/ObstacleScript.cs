using UnityEngine;
using System.Collections.Generic;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<AudioClip>  audios= new List<AudioClip>();
    [SerializeField] float downwardForce = 1f;
    [SerializeField] int minSpeed;
    [SerializeField] int maxSpeed;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        downwardForce = Random.Range(minSpeed, maxSpeed + 1);
        mainCamera = Camera.main; // Reference to the main camera
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
        AudioClip sfx = audios[Random.Range(0, audios.Count)];
        
        SoundManager.Instance.PlaySFX(sfx, 0.175f);

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
                    player.LoseLives();
                    Destroy(gameObject);

                }
            }
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Heyo");
        GameManager.instance.activeEntity.Remove(transform.gameObject);
    }
}
