using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // Player body is a child of this object that has three children: Head, Body, and Legs
    
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;

    [SerializeField] List<AudioClip> hitSound = new List<AudioClip>();
    [SerializeField] AudioClip DeathSound;
    [SerializeField] GameObject particleEffect;
    public FlashEffect flashEffect;
    [SerializeField] Rigidbody2D rb;
    public FuelScript fuel;
    [SerializeField] float HorizontalSpeed = 5f;
    [SerializeField] float VerticalSpeed = 5f;
    [SerializeField] float decelerationFactor = 0.95f; // How quickly the movement slows down
    private Vector3 currentVelocity = Vector3.zero; // Tracks current velocity
    private Vector3 movementInput = Vector3.zero; // Tracks movement input
    public int lives;
    public event Action onHit;
    public event Action onDeath;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] float InvincibilityTime = 1f;
    bool hasBeenHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        onHit += flashEffect.Flash;
        hasBeenHit = false;
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
            AudioClip clip = hitSound[UnityEngine.Random.Range(0, hitSound.Count)];
            SoundManager.Instance.PlaySFX(clip, 0.7f);
            onHit.Invoke();
            
        }
        if (lives <= 0)
        {
            //gameOver
            SoundManager.Instance.PlaySFX(DeathSound, 0.7f);
            GameManager.instance.LoseGame();
        }
        

    }
    public void RestHealth(int newlives)
    {
        lives = newlives;
        onDeath?.Invoke();
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
    public void UpdateTintMaterialSprites()
    {
        this.head.material.mainTexture = head.sprite.texture;
        this.body.material.mainTexture = body.sprite.texture;
        this.legs.material.mainTexture = legs.sprite.texture;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.attachedRigidbody != null && collision.attachedRigidbody.gameObject.CompareTag("Obstacle"))
            {
                // Get the point of impact
                Vector3 impactPoint = collision.ClosestPoint(transform.position);

                // Instantiate the particle effect at the impact point
                Instantiate(particleEffect, impactPoint, Quaternion.identity);
            }
        }
    }
}
