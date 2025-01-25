using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage = 10;
    [SerializeField] int MaxAmmoCapacity = 3;
    [SerializeField] int AmmoCapacity = 3;

    [SerializeField] float bulletRegenTime = 2f; // Time to regenerate one bullet
    private float regenTimer = 0f;

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
        if (AmmoCapacity < MaxAmmoCapacity)
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
