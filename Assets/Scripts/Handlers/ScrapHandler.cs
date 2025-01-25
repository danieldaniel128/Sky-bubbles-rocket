using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrapHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image _scrapImage;
    [SerializeField] ScrapDataSO _crupDataSO;


    [SerializeField] private Material CommonMaterial;
    [SerializeField] private Material RareMaterial;
    [SerializeField] private Material EpicMaterial;
    [SerializeField] private Material LegendaryMaterial;
    [Header("for readonly")]
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] private Canvas canvas;
    public ScrapsCollector ScrapsCollector;
    private Vector2 pointerOffset; // Offset between the mouse click and the object's position
    private RectTransform parentRect;
    bool isDragged = false;
    public Transform ScrapCreatedPosParent { get; set; }
    public Sprite _scrapSprite
    {
        get
        {
            if (_scrapImage != null)
                return _scrapImage.sprite; // Add 'return' to return the sprite
            else
                return null; // Explicitly return null if _scrapImage is null
        }
    }
    public ScrapDataSO ScrupDataSO { get { return _crupDataSO; } private set { _crupDataSO = value; } }
    public RocketScrapType ScrapType { get; private set; }
    //events
    public Action<ScrapHandler> OnScrapCollected;
    
    public void SetScrap(ScrapDataSO scrapDataSO,Transform scrapParent)
    {
        ScrapCreatedPosParent = scrapParent;
        SetScrapData(scrapDataSO);
        _rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        parentRect = scrapParent as RectTransform;
    }
    

    private void SetScrapData(ScrapDataSO scrapDataSO)
    {
        //set scrap data.
        ScrupDataSO = scrapDataSO;
        ScrapType = scrapDataSO.ScrapType;
        //set scrap sprite.
        SetScrapSprite(scrapDataSO.ScrapIcon);
        //set scrap gameobject name in hierarchy.
        gameObject.name = scrapDataSO.name;
    }
    private void SetScrapSprite(Sprite scrapSprite)
    {
        _scrapImage.sprite = scrapSprite;
        _scrapImage.SetNativeSize();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
        this.ScrapsCollector.StartCollectProcess(this);
        // Calculate the offset between the mouse pointer and the object's position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect, // Use the parent's RectTransform
            eventData.position,
            eventData.pressEventCamera, // Pass the camera for Screen Space - Camera mode
            out pointerOffset
        );
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_rectTransform != null && canvas != null)
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, // Use the parent's RectTransform
                eventData.position,
                eventData.pressEventCamera, // Pass the camera for Screen Space - Camera mode
                out localMousePosition
            );

            // Apply the offset to the position
            _rectTransform.anchoredPosition = localMousePosition - pointerOffset;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragged)
        {
            OnScrapCollected?.Invoke(this);
            isDragged = false;
            Debug.Log($"<color=blue>eneded drop {gameObject.name}</color>");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeMaterial();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_scrapImage != null)
            _scrapImage.material = null;
    }

    public void ChangeMaterial()
    {
        if (_scrapImage != null && _crupDataSO != null)
        {
                switch (_crupDataSO.Rarity)
                {
                    case RocketScrapRarity.Common:
                        _scrapImage.material = CommonMaterial;
                        break;
                    case RocketScrapRarity.Rare:
                    _scrapImage.material = RareMaterial;
                        break;
                    case RocketScrapRarity.Epic:
                        _scrapImage.material = EpicMaterial;
                        break;
                    default:
                        break;
                }
            Debug.Log($"Material changed to {_crupDataSO.Rarity}");
        }
        else
        {
            Debug.LogWarning("UI Image or Material is not assigned!");
        }
    }
    private void OnApplicationQuit()
    {
        OnScrapCollected = null;
    }
    private void OnDestroy()
    {
        OnScrapCollected = null;
    }
}

