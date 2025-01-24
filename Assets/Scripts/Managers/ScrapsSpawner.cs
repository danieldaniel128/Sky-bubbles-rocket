using System.Collections.Generic;
using UnityEngine;

public class ScrapsSpawner : MonoBehaviour
{
    [SerializeField] ScrapHandler _scrapPrefab;
    
    [Header("generated scraps types and possible positions")]
    [SerializeField] List<ScrapDataSO> _scrupDataSO;
    [SerializeField] List<Transform> _generatedScrapsPositions;

    [Header("References")]
    [SerializeField] ScrapsCollector _scrapCollector;

    [Header("Probabilities")]
    [SerializeField,Range(0, 1)] float _commonProbability;
    [SerializeField, Range(0, 1)] float _rareProbability;
    [SerializeField, Range(0, 1)] float _epicProbability;

    private void Start()
    {
        InitScraps();
    }
    void InitScraps()
    {
        foreach (Transform scrapsPosition in _generatedScrapsPositions)
        {
            //create scrap and position it.
            ScrapHandler scrapHandler = Instantiate(_scrapPrefab, scrapsPosition.position,Quaternion.identity, scrapsPosition);
            //set random scrap data from datas list
            ScrapDataSO randomScrapData = _scrupDataSO[Random.Range(0, _scrupDataSO.Count)];
            scrapHandler.SetScrap(randomScrapData, scrapsPosition);
            scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
        }
    }
}
