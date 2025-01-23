using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]float downwardForce = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(0, -downwardForce * Time.deltaTime);
        rb.linearVelocity = movement;
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
                    Destroy(gameObject);
                }
            }
        }
    }
}
