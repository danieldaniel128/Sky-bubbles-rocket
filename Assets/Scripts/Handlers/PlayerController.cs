using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player body Is a child of this object that has three children: Head, Body, and Legs
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] FuelScript fuel;
    [SerializeField]float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0);
        rb.linearVelocity = movement;
    }
    public void ReFuel(float amount)
    {
        if (fuel != null)
        {
            fuel.Refuel(amount);
        }
    }

}
