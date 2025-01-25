using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrapsCollector : MonoBehaviour
{
    [Header("positions for selected rocket parts to snap to, and their images")]
    [SerializeField] Image _head;
    [SerializeField] Image _thrusters;
    [SerializeField] Image _body;
    [Header("sprites for drag and drop")]
    [SerializeField] Sprite _dropToHead;
    [SerializeField] Sprite _dropToThrusters;
    [SerializeField] Sprite _dropToBody;
    [SerializeField] Sprite _originalHead;
    [SerializeField] Sprite _originalThrusters;
    [SerializeField] Sprite _originalBody;
    [SerializeField] Transform _headPos;
    [SerializeField] Transform _thrustersPos;
    [SerializeField] Transform _bodyPos;
    [Header("for readonly use (dont touch)")]
    [SerializeField] ScrapHandler _collectedHead;
    [SerializeField] ScrapHandler _collectedThrusters;
    [SerializeField] ScrapHandler _collectedBody;
    [Header("Finish Building")]
    [SerializeField] GameObject _launchButton;
    [SerializeField] float snapOffset = 50f;
    public void TryToCollectScrap(ScrapHandler collectedScrap)
    {
        // Define a snapping offset distance
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:
                HandleSnapOrRelease(collectedScrap, ref _collectedBody, _bodyPos.position, snapOffset);
                break;

            case RocketScrapType.Thrusters:
                HandleSnapOrRelease(collectedScrap, ref _collectedThrusters, _thrustersPos.position, snapOffset);
                break;

            case RocketScrapType.Head:
                HandleSnapOrRelease(collectedScrap, ref _collectedHead, _headPos.position, snapOffset);
                break;

            default:
                Debug.Log("<color=red>Something went wrong</color>");
                break;
        }
    }
    public void StartCollectProcess(ScrapHandler collectedScrap)
    {
        // Define a snapping offset distance
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:
                _body.sprite = _dropToBody;
                _body.SetNativeSize();
                break;

            case RocketScrapType.Thrusters:
                _thrusters.sprite = _dropToThrusters;
                _thrusters.SetNativeSize();
                break;

            case RocketScrapType.Head:
                _head.sprite = _dropToHead;
                _head.SetNativeSize();
                break;

            default:
                Debug.Log("<color=red>Something went wrong</color>");
                break;
        }
    }
    public void EndCollectProcess(ScrapHandler collectedScrap)
    {
        // Define a snapping offset distance
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:
                if (_collectedBody == null)
                {
                    _body.sprite = _originalBody;
                    _body.SetNativeSize();
                }
                else
                {
                    _body.sprite = _dropToBody;
                    _body.SetNativeSize();
                }
                break;

            case RocketScrapType.Thrusters:
                if (_collectedBody == null)
                { 
                    _thrusters.sprite = _originalThrusters;
                    _thrusters.SetNativeSize();
                }
                else
                {
                    _thrusters.sprite = _dropToThrusters;
                    _thrusters.SetNativeSize();
                }
                break;

            case RocketScrapType.Head:
                if (_collectedBody == null)
                {
                    _head.sprite = _originalHead;
                    _head.SetNativeSize();
                }
                else
                {
                    _head.sprite = _dropToHead;
                    _head.SetNativeSize();
                }
                break;

            default:
                Debug.Log("<color=red>Something went wrong</color>");
                break;
        }
    }
    public void ResetCollectingImages()
    {
        _body.sprite = _originalBody;
        _body.SetNativeSize();
        _thrusters.sprite = _originalThrusters;
        _thrusters.SetNativeSize();
        _head.sprite = _originalHead;
        _head.SetNativeSize();
        _head.enabled = true;
        _thrusters.enabled = true;
        _body.enabled = true;
    }

    private void HandleSnapOrRelease(ScrapHandler collectedScrap, ref ScrapHandler collectedSlot, Vector3 targetPos, float snapOffset)
    {
        // Calculate the distance between the collected scrap and the target position
        float distance = Vector2.Distance(
            new Vector2(collectedScrap.transform.position.x, collectedScrap.transform.position.y),
            new Vector2(targetPos.x, targetPos.y)
        );
        Debug.Log($"<color=green>distanced ended drop: {distance}</color>");
        if (distance <= snapOffset && collectedSlot == null)
        {
            // Snap to target position
            collectedScrap.transform.position = targetPos;

            // Assign the collected scrap to the corresponding slot
            if (collectedSlot != collectedScrap)
            {
                AddToCollected(ref collectedSlot, collectedScrap);
                //close drag and drop image.
                collectedScrap.transform.parent.GetComponent<Image>().enabled = false;
                EndCollectProcess(collectedScrap);
            }

            Debug.Log($"<color=green>{collectedScrap.name} snapped to {targetPos}</color>");
        }
        else
        {
            //before releasing return the parent image
            collectedScrap.transform.parent.GetComponent<Image>().enabled = true;
            // Release the scrap
            ReleaseFromCollected(collectedScrap, ref collectedSlot);
            Debug.Log($"<color=yellow>{collectedScrap.name} released from {targetPos}</color>");
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
        releasedCollectedScrap.transform.position = releasedCollectedScrap.ScrapCreatedPosParent.position - Vector3.forward * 10;
        releasedCollectedScrap.transform.SetParent(releasedCollectedScrap.ScrapCreatedPosParent);
        //clear only if it droped out from rocket body. if its dragged scrap.
        if (releasedCollectedScrap == collectedScrap)
        {
            collectedScrap = null;
            _launchButton.SetActive(false);
        }
        EndCollectProcess(releasedCollectedScrap);
        Debug.Log($"<color=red>Released Scrap {releasedCollectedScrap.name}</color>");
        Debug.Log($"<color=red>Released Scrap new parent {releasedCollectedScrap.ScrapCreatedPosParent.name}</color>");
    }
    private void UpdateRocketBodyParts()
    {
        Vector3 Offset = -Vector3.forward * 10;
        if (_collectedHead != null)
        {
            //set to part position
            _collectedHead.transform.position = _headPos.position + Offset;

            //set new parent.
            _collectedHead.transform.SetParent(_head.transform);
        }
        if (_collectedThrusters != null)
        {
            //set to part position
            _collectedThrusters.transform.position = _thrustersPos.position + Offset;
            //set new parent.
            _collectedThrusters.transform.SetParent(_thrusters.transform);
        }
        if (_collectedBody != null)
        { 
            //set to part position
            _collectedBody.transform.position = _bodyPos.position + Offset;
            //set new parent.
            _collectedBody.transform.SetParent(_body.transform);
        }
        if (_collectedBody != null && _collectedThrusters != null && _collectedHead != null)
        { 
            _launchButton.SetActive(true);
            GameManager.instance.PlayerController.SetParts(_collectedHead._scrapSprite, _collectedBody._scrapSprite, _collectedThrusters._scrapSprite);
            GameManager.instance.PlayerController.RestHealth((_collectedHead.ScrupDataSO as RocketHead).Lives);
            GameManager.instance.PlayerController.fuel.Setfuel((_collectedThrusters.ScrupDataSO as RocketThrusters).Fuel);
        }
    }
    private void OnEnable()
    {
        _launchButton.SetActive(false);

        _collectedHead = null;
        _collectedBody = null;
        _collectedThrusters = null;
    }
}
