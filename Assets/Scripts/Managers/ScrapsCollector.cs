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
    [Header("Finish Building")]
    [SerializeField] GameObject _launchButton;
    public void TryToCollectScrap(ScrapHandler collectedScrap)
    {
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:
                //check if the collected is already collected.
                if (_collectedBody != collectedScrap)
                    AddToCollected(ref _collectedBody, collectedScrap);
                else//already got collected, release it.
                    ReleaseFromCollected(collectedScrap, ref _collectedBody);
                break;
            case RocketScrapType.Thrusters:
                if (_collectedThrusters != collectedScrap)
                    AddToCollected(ref _collectedThrusters, collectedScrap);
                else
                    ReleaseFromCollected(collectedScrap, ref _collectedThrusters);
                break;
            case RocketScrapType.Head:
                if (_collectedHead != collectedScrap)
                    AddToCollected(ref _collectedHead, collectedScrap);
                else
                    ReleaseFromCollected(collectedScrap, ref _collectedHead);
                break;
            default:
                Debug.Log("<color=red>something went wrong</color>");
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
    private void ReleaseFromCollected(ScrapHandler releasedCollectedScrap, ref ScrapHandler collectedScrap)
    {
        releasedCollectedScrap.transform.position = releasedCollectedScrap.ScrapCreatedPosParent.position;
        releasedCollectedScrap.transform.SetParent(releasedCollectedScrap.ScrapCreatedPosParent);
        collectedScrap = null;
        _launchButton.SetActive(false);
        Debug.Log($"<color=red>Released Scrap {releasedCollectedScrap.name}</color>");
        Debug.Log($"<color=red>Released Scrap new parent {releasedCollectedScrap.ScrapCreatedPosParent.name}</color>");
    }
    private void UpdateRocketBodyParts()
    {
        if (_collectedHead != null)
        {
            //set to part position
            _collectedHead.transform.position = _headPos.position;
            //set new parent.
            _collectedHead.transform.SetParent(_headPos);
        }
        if (_collectedThrusters != null)
        {
            //set to part position
            _collectedThrusters.transform.position = _thrustersPos.position;
            //set new parent.
            _collectedThrusters.transform.SetParent(_thrustersPos);
        }
        if (_collectedBody != null)
        { 
            //set to part position
            _collectedBody.transform.position = _bodyPos.position;
            //set new parent.
            _collectedBody.transform.SetParent(_bodyPos);
        }
        if (_collectedBody != null && _collectedThrusters != null && _collectedHead != null)
        { 
            _launchButton.SetActive(true);
            GameManager.instance.PlayerController.SetParts(_collectedHead._scrapSprite, _collectedBody._scrapSprite, _collectedThrusters._scrapSprite);
        }
    }
}
