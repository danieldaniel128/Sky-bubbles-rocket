using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private List<RectTransform> backgrounds; // List of background UI elements
    [SerializeField,Range(0,2)] private float scrollSpeed; // Speed at which the backgrounds scroll in pixels per second
    [SerializeField] private Canvas canvas; // Reference to the canvas
    bool finishedLaunch = false;
    private float screenHeight;
    private Vector2[] originalPositions;

    [SerializeField] private List<Sprite> _propsForBackgroundSprites; // List of background UI elements
    [SerializeField] private List<Image> _propsForBackgroundPositions; // List of background UI elements
    [SerializeField] RectTransform _propsContainer;
    [SerializeField] GameObject _rocketUI;
    [SerializeField] GameObject _bathUI;
    RectTransform highestBackground;
    private void Start()
    {
        // Get the screen height in pixels based on the canvas scaler
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        screenHeight = canvasScaler.referenceResolution.y;
        // Store original positions of the backgrounds
        originalPositions = new Vector2[backgrounds.Count];
        for (int i = 0; i < backgrounds.Count; i++)
        {
            originalPositions[i] = backgrounds[i].anchoredPosition;
        }
    }
    private void OnDisable()
    {
        finishedLaunch = false;
    }
    public void ActivateScrol()
    {
        _rocketUI.SetActive(false);
        this.enabled = true;

    }
    [ContextMenu("DeactivateScrol")]
    public void DeactivateScrol()
    {
        this.enabled = false;
        _bathUI.SetActive(true);
        _rocketUI.SetActive(true);
        ResetBackgroundPositions();


        
    }
    private void Update()
    {
        ScrollBackgrounds();
    }

    int passedTwice = 1; 
    private void ScrollBackgrounds()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            float backgroundHeight = backgrounds[i].sizeDelta.y;

            // Check if the background is below the screen
            if (backgrounds[i].anchoredPosition.y < -backgroundHeight)
            {
                // Find the highest background currently
                highestBackground = GetHighestBackground();

                // Move the current background above the highest one precisely
                backgrounds[i].anchoredPosition = new Vector2(
                    highestBackground.anchoredPosition.x,
                    highestBackground.anchoredPosition.y + backgroundHeight
                );
                //here is where the top is moved to
                // Assign random props to the background that is now on top
                if(passedTwice % 4 == 0)
                {
                    AssignRandomPropsToBackground();
                    passedTwice = 1;
                    Debug.Log("random prop now");
                }

                //is the first background of bath
                if (!finishedLaunch)
                {
                    _bathUI.SetActive(false);
                    finishedLaunch = true;
                    
                }
                passedTwice++;
            }
            // Move each background down in pixels
            backgrounds[i].anchoredPosition += Vector2.down * scrollSpeed *1080 * Time.deltaTime;

            // Get the actual height of the background
        }
    }
    private void AssignRandomPropsToBackground()
    {
        if (_propsForBackgroundSprites.Count == 0 || _propsForBackgroundPositions.Count == 0)
        {
            Debug.LogWarning("No props or positions available for assignment.");
            return;
        }

        foreach (var propPosition in _propsForBackgroundPositions)
        {
            // Select a random sprite
            Sprite randomSprite = _propsForBackgroundSprites[Random.Range(0, _propsForBackgroundSprites.Count)];

            // Assign the random sprite to the image component
            Image imageComponent = propPosition;
            if (imageComponent != null)
            {
                // Set the highestBackground as the parent
                _propsContainer.SetParent(highestBackground);

                // Center the image on the Y-axis
                RectTransform imageRectTransform = imageComponent.GetComponent<RectTransform>();
                if (imageRectTransform != null)
                {
                    // Reset position relative to the parent
                    imageRectTransform.anchoredPosition = new Vector2(imageRectTransform.anchoredPosition.x, 0);
                    imageRectTransform.localScale = Vector3.one; // Reset scale in case it's altered
                }

                // Assign the random sprite and set the native size
                imageComponent.sprite = randomSprite;
                imageComponent.SetNativeSize();

                Debug.Log($"Assigned sprite {randomSprite.name} to {imageComponent.name} and centered it on Y-axis.");
            }
            else
            {
                Debug.LogWarning($"No Image component found on {propPosition.name}");
            }

        }
        _propsContainer.SetParent(highestBackground);

        // Center the image on the Y-axis
        if (_propsContainer != null)
        {
            // Reset position relative to the parent
            _propsContainer.anchoredPosition = new Vector2(_propsContainer.anchoredPosition.x, 0);
            _propsContainer.localScale = Vector3.one; // Reset scale in case it's altered
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
    private void ResetBackgroundPositions()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].anchoredPosition = originalPositions[i];
        }
        //reset props positions
        foreach (var propPosition in _propsForBackgroundPositions)
        {
            // Assign the random sprite to the image component
            Image imageComponent = propPosition;
            if (imageComponent != null)
            {
                // Set the highestBackground as the parent
                imageComponent.transform.SetParent(backgrounds[1]);

                // Center the image on the Y-axis
                RectTransform imageRectTransform = imageComponent.GetComponent<RectTransform>();
                if (imageRectTransform != null)
                {
                    // Reset position relative to the parent
                    imageRectTransform.anchoredPosition = new Vector2(imageRectTransform.anchoredPosition.x, 0);
                    imageRectTransform.localScale = Vector3.one; // Reset scale in case it's altered
                }
            }
            else
            {
                Debug.LogWarning($"No Image component found on {propPosition.name}");
            }

        }
    }
}
