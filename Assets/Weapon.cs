using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage = 10;
    [SerializeField] int AmmoCapacity = 3;
    public int MaxAmmocapacity = 3; // Getter for Max Ammo Capacity
    public int Ammocapacity => AmmoCapacity;       // Getter for Current Ammo Capacity

    [SerializeField] float bulletRegenTime = 2f; // Time to regenerate one bullet
    private float regenTimer = 0f;
    private void OnEnable()
    {
        AmmoCapacity = MaxAmmocapacity;
    }
    void Update()
    {
        // Check for shooting input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // Call bullet regeneration logic
        RegenBullet();
    }

    

    public float GetRegenTimer()
    {
        return regenTimer;
    }

    public float GetRegenTime()
    {
        return bulletRegenTime;
    }
    private void Shoot()
    {
        if (AmmoCapacity > 0)
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            AmmoCapacity--; // Decrease ammo capacity
        }
        else
        {
            Debug.Log("No ammo left!");
        }
    }

    private void RegenBullet()
    {
        // Only regenerate if ammo is below the max capacity
        if (AmmoCapacity < MaxAmmocapacity)
        {
            regenTimer += Time.deltaTime;

            // Add one ammo if the regen timer exceeds the bullet regen time
            if (regenTimer >= bulletRegenTime)
            {
                AmmoCapacity++;
                regenTimer = 0f; // Reset the timer
                Debug.Log("Ammo regenerated. Current Ammo: " + AmmoCapacity);
            }
        }
    }
}
