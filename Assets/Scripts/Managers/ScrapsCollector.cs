using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrapsCollector : MonoBehaviour
{
    [Header("positions for selected rocket parts to snap to")]
    [SerializeField] Transform _headPos;
    [SerializeField] Transform _thrustersPos;
    [SerializeField] Transform _bodyPos;
    [Header("for readonly use (dont touch)")]
    [SerializeField] ScrapHandler _collectedHead;
    [SerializeField] ScrapHandler _collectedThrusters;
    [SerializeField] ScrapHandler _collectedBody;
    public void TryToCollectScrap(ScrapHandler collectedScrap)
    {
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:
                AddToCollected(ref _collectedBody, collectedScrap);
                break;
            case RocketScrapType.Thrusters:
                AddToCollected(ref _collectedThrusters, collectedScrap);
                break;
            case RocketScrapType.Head:
                AddToCollected(ref _collectedHead, collectedScrap);
                break;
            default:
                Debug.Log("something went wrong");
                break;
        }
    }
    private void AddToCollected(ref ScrapHandler collectedScrap,ScrapHandler tryingToCollectScrap) 
    {
        if (collectedScrap != null)
            return;
        collectedScrap = tryingToCollectScrap;
        UpdateRocketBodyParts();
        Debug.Log($"<color=green>Collected Scrap {collectedScrap.name}</color>");
    }
    private void UpdateRocketBodyParts()
    {
        if(_collectedHead != null)
            _collectedHead.transform.position = _headPos.position;
        if (_collectedThrusters != null)
            _collectedThrusters.transform.position = _thrustersPos.position;
        if (_collectedBody != null)
            _collectedBody.transform.position = _bodyPos.position;
    }
}
