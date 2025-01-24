using System.Collections;
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
    [SerializeField] List<ScrapHandler> _allScrapCreated;

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
    private void InitScraps()
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
            randomScrapData = GetRandomScrapByRarity(_allScrapDataSO);
            scrapHandler.SetScrap(randomScrapData, scrapsPosition);
            scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
            _allScrapCreated.Add(scrapHandler);
        }
        _generatedScrapsPositions.Clear();
    }
    private ScrapDataSO GetRandomScrapByRarity(List<ScrapDataSO> fromList)
    {
        // Get a random value to determine the rarity
        float randomValue = Random.value;

        // Determine the rarity based on the probabilities
        RocketScrapRarity selectedRarity;
        if (randomValue <= _commonProbability)
        {
            selectedRarity = RocketScrapRarity.Common;
        }
        else if (randomValue <= _commonProbability + _rareProbability)
        {
            selectedRarity = RocketScrapRarity.Rare;
        }
        else
        {
            selectedRarity = RocketScrapRarity.Epic;
        }

        // Filter the scraps by the selected rarity
        List<ScrapDataSO> filteredScraps = fromList.FindAll(scrap => scrap.Rarity == selectedRarity);

        // If there are no scraps of the selected rarity, return null
        if (filteredScraps.Count == 0)
        {
            Debug.LogWarning($"No scraps available for rarity: {selectedRarity}");
            return null;
        }

        // Return a random scrap from the filtered list
        return filteredScraps[Random.Range(0, filteredScraps.Count)];
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
        randomScrapData = GetRandomScrapByRarity(_rocketBodyDataSO);
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
        _allScrapCreated.Add(scrapHandler);

        randomPos = GetRandomPosAndOccupie();
        //removed now and add to occupied.
        _occupiedPositions.Add(randomPos);
        scrapHandler = Instantiate(_scrapPrefab, randomPos.position, Quaternion.identity, randomPos);
        //set random scrap data from datas list
        randomScrapData = GetRandomScrapByRarity(_rocketHeadDataSO);
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
        _allScrapCreated.Add(scrapHandler);

        randomPos = GetRandomPosAndOccupie();
        //
        //removed now and add to occupied.
        _occupiedPositions.Add(randomPos);
        scrapHandler = Instantiate(_scrapPrefab, randomPos.position, Quaternion.identity, randomPos);
        //set random scrap data from datas list
        randomScrapData = GetRandomScrapByRarity(_rocketThrustersDataSO);
        scrapHandler.SetScrap(randomScrapData, randomPos);
        scrapHandler.OnScrapCollected += _scrapCollector.TryToCollectScrap;
        _allScrapCreated.Add(scrapHandler);
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
    public void InitScrapsCoro() 
    {
        StartCoroutine(CreateNewScraps());
    }
    private void ClearDataFromLastGame()
    {
        if(_occupiedPositions.Count>0)
        for (int i = 0; i < _occupiedPositions.Count; i++)
        {
            _generatedScrapsPositions.Add(_occupiedPositions[i]);
        }
        foreach (ScrapHandler obj in _allScrapCreated)
        {
            Debug.Log("destroyed created scrp from last game");
            obj.gameObject.SetActive(false);
        }
        Debug.Log("cleared data from last game");
        _allScrapCreated.Clear();
        _occupiedPositions.Clear();
    }
    private IEnumerator CreateNewScraps()
    {
        ClearDataFromLastGame();
        yield return new WaitForEndOfFrame(); // Wait for destruction to complete
        yield return new WaitForSeconds(0.1f);
        InitScraps();
    }
}
