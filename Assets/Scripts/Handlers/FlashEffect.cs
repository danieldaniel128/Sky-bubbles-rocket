using UnityEngine;
using System.Collections.Generic;

public class FlashEffect : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<Material> originalMaterials = new List<Material>();
    [SerializeField] private Material whiteFlashMaterial; // Flash material
    public float flashDuration = 2f; // Total time for flashing
    public float flashInterval = 0.4f; // Time between each flash

    private bool isFlashing = false; // Tracks if the flash is active
    private float flashTimer = 0f; // Timer for flashing intervals
    private float durationTimer = 0f; // Timer for total flash duration
    private bool useOriginalMaterial = false; // Toggle material state

    private void Start()
    {
        // Store the original materials of each SpriteRenderer
        foreach (var sr in spriteRenderers)
        {
            originalMaterials.Add(sr.material);
        }
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            Debug.Log("Game Over");
            StopFlash(); // Stop flashing on game over
            return;
        }
        else if (isFlashing)
        {
            HandleFlashing();
        }
    }

    public void Flash()
    {
        if (spriteRenderers.Count > 0 && whiteFlashMaterial != null)
        {
            isFlashing = true;
            flashTimer = 0f;
            durationTimer = 0f;
        }
    }

    public void StopFlash()
    {
        // Completely stop flashing
        isFlashing = false;
        flashTimer = 0f;
        durationTimer = 0f;

        // Restore the original materials immediately
        RestoreOriginalMaterials();
    }

    private void HandleFlashing()
    {
        // Update the total duration timer
        durationTimer += Time.deltaTime;

        // Stop flashing if the total duration exceeds the flash duration
        if (durationTimer >= flashDuration)
        {
            StopFlash();
            return;
        }

        // Update the flash interval timer
        flashTimer += Time.deltaTime;

        // Toggle materials when the interval timer exceeds the flash interval
        if (flashTimer >= flashInterval)
        {
            flashTimer = 0f; // Reset the interval timer
            useOriginalMaterial = !useOriginalMaterial;

            // Apply the toggled material to all SpriteRenderers
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].material = useOriginalMaterial ? originalMaterials[i] : whiteFlashMaterial;
            }
        }
    }

    private void RestoreOriginalMaterials()
    {
        // Ensure all SpriteRenderers return to their original materials
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].material = originalMaterials[i];
        }
    }
}
