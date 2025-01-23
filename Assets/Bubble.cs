using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]float downwardForce = 1f;
    [SerializeField] int minSpeed;
    [SerializeField] int maxSpeed; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        downwardForce = Random.Range(minSpeed, maxSpeed+1);
        //downwardForce *= 20;
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
                    Destroy(gameObject);
                }
            }
        }
    }
}
