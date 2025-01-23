using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrapHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image _scrapImage;
    [SerializeField] ScrapDataSO _crupDataSO;

    public Transform _scrapParent;
    public ScrapDataSO ScrupDataSO { get { return _crupDataSO; } private set { _crupDataSO = value; } }
    public RocketScrapType ScrapType { get; private set; }
    //events
    public Action<ScrapHandler> OnScrapCollected;
    public void SetScrap(ScrapDataSO scrapDataSO,Transform scrapParent)
    {
        _scrapParent = scrapParent;
        SetScrapData(scrapDataSO);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnScrapCollected?.Invoke(this);
        Debug.Log($"<color=green>scrap {gameObject.name} has been clicked</color>");
    }

    private void SetScrapData(ScrapDataSO scrapDataSO)
    {
        //set scrap data.
        ScrupDataSO = scrapDataSO;
        //set scrap sprite.
        SetScrapSprite(scrapDataSO.ScrapIcon);
        //set scrap gameobject name in hierarchy.
        gameObject.name = scrapDataSO.name;
    }
    private void SetScrapSprite(Sprite scrapSprite)
    {
        _scrapImage.sprite = scrapSprite;
    }
    
}
