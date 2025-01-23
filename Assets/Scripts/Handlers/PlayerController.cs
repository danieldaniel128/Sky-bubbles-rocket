using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Player body is a child of this object that has three children: Head, Body, and Legs
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;

    [SerializeField] Rigidbody2D rb;
    public FuelScript fuel;
    [SerializeField] float HorizontalSpeed = 5f;
    [SerializeField] float VerticalSpeed = 5f;
    [SerializeField] float decelerationFactor = 0.95f; // How quickly the movement slows down
    private Vector3 currentVelocity = Vector3.zero; // Tracks current velocity
    private Vector3 movementInput = Vector3.zero; // Tracks movement input
    public int lives;
    public event Action onHit;
    [SerializeField] CameraShake cameraShake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input
        movementInput = new Vector3(
            Input.GetAxisRaw("Horizontal") * HorizontalSpeed,
            Input.GetAxisRaw("Vertical") * VerticalSpeed,
            0
        );

        if (movementInput.magnitude > 0)
        {
            // Directly set velocity to match input direction
            currentVelocity = movementInput;
        }
        else
        {
            // Apply deceleration when no input is given
            currentVelocity *= decelerationFactor;
        }

        // Apply velocity to position
        transform.position += currentVelocity * Time.deltaTime;
    }
    

    public void ReFuel(float amount)
    {
        if (fuel != null)
        {
            fuel.Refuel(amount);
        }
    }
    public void LoseLives()
    {
        lives--;
        if (lives <= 0)
        {
            //gameOver
        }
        onHit.Invoke();
        cameraShake.StartShake();

    }
}
