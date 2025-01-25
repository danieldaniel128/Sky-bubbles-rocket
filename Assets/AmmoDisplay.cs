using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Weapon weapon; // Reference to the Weapon script
    [SerializeField] private Image[] ammoImages; // Array of UI Images representing ammo slots

    void Update()
    {
        if (weapon == null || ammoImages == null || ammoImages.Length == 0) return;

        // Loop through each ammo slot
        for (int i = 0; i < weapon.MaxAmmocapacity; i++)
        {
            if (i < weapon.Ammocapacity)
            {
                // Fully filled ammo slots
                ammoImages[i].fillAmount = 1f;
            }
            else if (i == weapon.Ammocapacity)
            {
                // Partially filled ammo slot (current regenerating)
                float progress = Mathf.Clamp01(weapon.GetRegenTimer() / weapon.GetRegenTime());
                ammoImages[i].fillAmount = progress;
            }
            else
            {
                // Empty ammo slots
                ammoImages[i].fillAmount = 0f;
            }
        }
    }
}
