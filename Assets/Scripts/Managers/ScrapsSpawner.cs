using System.Collections.Generic;
using UnityEngine;

public class ScrapsSpawner : MonoBehaviour
{
    [SerializeField] ScrapHandler _scrapPrefab;
    
    [Header("generated scraps types and possible positions")]
    [Header("RocketBodys")]
    [SerializeField] List<ScrapDataSO> _rocketBodyDataSO;
    [Header("RocketHeads")]
    [SerializeField] List<ScrapDataSO> _rocketHeadDataSO;
    [Header("RocketThrusters")]
    [SerializeField] List<ScrapDataSO> _rocketThrustersDataSO;

    [Header("All Scraps, readonly")]
    [SerializeField] List<ScrapDataSO> _allScrapDataSO;

    [Header("scraps positions")]
    [SerializeField] List<Transform> _generatedScrapsPositions;
    [SerializeField] List<Transform> _occupiedPositions;

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
        //clean it from last session.
        _allScrapDataSO.Clear();
        //all scraps to one list.
        _allScrapDataSO.AddRange(_rocketBodyDataSO);
        _allScrapDataSO.AddRange(_rocketHeadDataSO);
        _allScrapDataSO.AddRange(_rocketThrustersDataSO);
        GenerateOneOfEachType();
        //generate randomly with propebility of rarities.
        foreach (Transform scrapsPosition in _generatedScrapsPositions)
        {
            ScrapHandler scrapHandler;
            ScrapDataSO randomScrapData;
            //removed now and add to occupied.
            _occupiedPositions.Add(scrapsPosition);
            scrapHandler = Instantiate(_scrapPrefab, scrapsPosition.position, Quaternion.identity, scrapsPosition);
            //set random scrap data from datas list
            randomScrapData = _rocketBodyDataSO[Random.Range(0, _rocketBodyDataSO.Count)];
            scrapHandler.SetScrap(randomScrapData, scrapsPosition);
            scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
        }
    }
    public void GenerateOneOfEachType()
    {
        Transform randomPos;
        ScrapHandler scrapHandler;
        ScrapDataSO randomScrapData;
        randomPos = GetRandomPosAndOccupie();
        //removed now and add to occupied.
        _occupiedPositions.Add(randomPos);
        scrapHandler = Instantiate(_scrapPrefab, randomPos.position, Quaternion.identity, randomPos);
        //set random scrap data from datas list
        randomScrapData = _rocketBodyDataSO[Random.Range(0, _rocketBodyDataSO.Count)];
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;

        randomPos = GetRandomPosAndOccupie();
        //removed now and add to occupied.
        _occupiedPositions.Add(randomPos);
        scrapHandler = Instantiate(_scrapPrefab, randomPos.position, Quaternion.identity, randomPos);
        //set random scrap data from datas list
        randomScrapData = _rocketHeadDataSO[Random.Range(0, _rocketHeadDataSO.Count)];
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;

        randomPos = GetRandomPosAndOccupie();
        //
        //removed now and add to occupied.
        _occupiedPositions.Add(randomPos);
        scrapHandler = Instantiate(_scrapPrefab, randomPos.position, Quaternion.identity, randomPos);
        //set random scrap data from datas list
        randomScrapData = _rocketThrustersDataSO[Random.Range(0, _rocketThrustersDataSO.Count)];
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
    }
    private Transform GetRandomPosAndOccupie()
    {
        Transform randomPos = _generatedScrapsPositions[Random.Range(0, _generatedScrapsPositions.Count)];
        while (!_generatedScrapsPositions.Remove(randomPos))
        {
            //got removed already
            randomPos = _generatedScrapsPositions[Random.Range(0, _generatedScrapsPositions.Count)];
        }
        return randomPos;
    }
}
