using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrapHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _scrapImage;
    [SerializeField] ScrapDataSO _crupDataSO;
    [SerializeField] private Material CommonMaterial;
    [SerializeField] private Material RareMaterial;
    [SerializeField] private Material EpicMaterial;
    [SerializeField] private Material LegendaryMaterial;
    
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
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnScrapCollected?.Invoke(this);
        Debug.Log($"<color=blue>clicked {gameObject.name}</color>");
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
    private void OnApplicationQuit()
    {
        OnScrapCollected = null;
    }
    private void OnDestroy()
    {
        OnScrapCollected = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_scrapImage != null)
            _scrapImage.material = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeMaterial();
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
}
