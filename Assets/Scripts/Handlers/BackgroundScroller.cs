using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private List<RectTransform> backgrounds; // List of background UI elements
    [SerializeField,Range(0,2)] private float scrollSpeed; // Speed at which the backgrounds scroll in pixels per second
    [SerializeField] private Canvas canvas; // Reference to the canvas

    private float screenHeight;

    private void Start()
    {
        // Get the screen height in pixels based on the canvas scaler
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        screenHeight = canvasScaler.referenceResolution.y;
    }

    private void Update()
    {
        ScrollBackgrounds();
    }

    private void ScrollBackgrounds()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            float backgroundHeight = backgrounds[i].sizeDelta.y;

            // Check if the background is below the screen
            if (backgrounds[i].anchoredPosition.y < -backgroundHeight)
            {
                // Find the highest background currently
                RectTransform highestBackground = GetHighestBackground();

                // Move the current background above the highest one precisely
                backgrounds[i].anchoredPosition = new Vector2(
                    highestBackground.anchoredPosition.x,
                    highestBackground.anchoredPosition.y + backgroundHeight
                );
            }
            // Move each background down in pixels
            backgrounds[i].anchoredPosition += Vector2.down * scrollSpeed *1080 * Time.deltaTime;

            // Get the actual height of the background
        }
    }

    private RectTransform GetHighestBackground()
    {
        RectTransform highest = backgrounds[0];

        foreach (RectTransform background in backgrounds)
        {
            if (background.anchoredPosition.y > highest.anchoredPosition.y)
            {
                highest = background;
            }
        }

        return highest;
    }
}
