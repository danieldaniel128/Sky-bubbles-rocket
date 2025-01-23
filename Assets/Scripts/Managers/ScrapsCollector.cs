using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrapsCollector : MonoBehaviour
{
    [SerializeField] ScrapHandler _collectedHead;
    [SerializeField] ScrapHandler _collectedhrusters;
    [SerializeField] ScrapHandler _collectedBody;
    public void TryToCollectScrap(ScrapHandler collectedScrap)
    {
        switch (collectedScrap.ScrapType)
        {
            case RocketScrapType.Body:

                break;
            case RocketScrapType.Thrusters:

                break;
            case RocketScrapType.Head:

                break;
            default:
                Debug.Log("something went wrong");
                break;
        }
    }
    private void AddToCollected(ScrapHandler collectedScrap) 
    {
    
    }
    private void SetCollectedByType()
    {
        
    }
}
