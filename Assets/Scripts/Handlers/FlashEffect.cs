using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<Material> originalMaterials = new List<Material>();
    [SerializeField] private Material whiteFlashMaterial; // Flash material
    public float flashDuration = 2f; // Total time for flashing
    public float flashInterval = 0.4f; // Time between each flash

    private void Start()
    {
        // Get all SpriteRenderer components in this GameObject and its children
     

        // Store the original materials of each SpriteRenderer
        foreach (var sr in spriteRenderers)
        {
            originalMaterials.Add(sr.material);
        }
    }

    public void Flash()
    {
        if (spriteRenderers.Count > 0 && whiteFlashMaterial != null)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        float elapsedTime = 0f;
        bool useOriginalMaterial = false;

        while (elapsedTime < flashDuration)
        {
            // Toggle between flash material and original material for each SpriteRenderer
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].material = useOriginalMaterial ? originalMaterials[i] : whiteFlashMaterial;
            }

            useOriginalMaterial = !useOriginalMaterial;
            elapsedTime += flashInterval;

            yield return new WaitForSeconds(flashInterval);
        }

        // Ensure all SpriteRenderers return to their original materials
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].material = originalMaterials[i];
        }
    }
}
