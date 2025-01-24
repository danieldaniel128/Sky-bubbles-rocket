using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifetime = 5f; // How long the projectile lives before being destroyed

    void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile upward (positive Y direction) in 2D space
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            // Destroy the projectile when it hits an enemy
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
