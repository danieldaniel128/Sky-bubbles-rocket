using UnityEngine;
using System;
using System.Collections;

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
    [SerializeField] float InvincibilityTime = 1f;
    bool hasBeenHit = false;

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
        if (!hasBeenHit)
        {
           hasBeenHit = true;
            lives--;
            StartCoroutine(RestDamageBool());
            onHit.Invoke();
        }
        if (lives <= 0)
        {
            //gameOver
        }
        

    }
    private IEnumerator RestDamageBool()
    {
        yield return new WaitForSeconds(InvincibilityTime);
        hasBeenHit = false;
    }
    public void SetParts(Sprite head,Sprite body,Sprite legs)
    {
        this.head.sprite = head;
        this.body.sprite = body;
        this.legs.sprite = legs;

    }
}
