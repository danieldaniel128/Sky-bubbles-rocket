using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ShopManager : MonoBehaviour
{
    [SerializeField] Button fuelButton;
    [SerializeField] Button coinButton;
    [SerializeField] Button RarityButton;
    [Header("Spawners")]
    [SerializeField] BubbleSpawner bubbleSpawner;
    [SerializeField] CoinSpawner coinSpawner;
    [SerializeField] ScrapsSpawner scrapsSpawner;
    [SerializeField] TextMeshProUGUI fuelCostText;
    [SerializeField] TextMeshProUGUI coinCostText;
    [SerializeField] TextMeshProUGUI rarityCostText;
    [SerializeField] TextMeshProUGUI coins;
    int numberOfFuelPurchese = 0;
    private float fuelCost = 20;
    private float coinCost = 20;
    private float rarityCost = 20;
    private float CostIncrease = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuelButton.onClick.AddListener(() => FuelIncreaseButton());
        coinButton.onClick.AddListener(() => CoinIncreaseButton());
        RarityButton.onClick.AddListener(() => IncreaseRarity());

    }
    void IncreaseRarity()
    {
        if (GameManager.instance.CoinManager.Coins >= rarityCost)
        {
            GameManager.instance.RemoveCoins((int)rarityCost);
            scrapsSpawner.UpgradeRarity(RarityButton);
            rarityCost *= CostIncrease;
            rarityCost = Mathf.Round(rarityCost);
            rarityCostText.text = rarityCost.ToString();
            UpdateCoin();
        }
        else
        {
            Debug.Log("Not Enough cash");
        }
    }
        void FuelIncreaseButton() {
    
        if (GameManager.instance.CoinManager.Coins>= fuelCost)
        {
            GameManager.instance.RemoveCoins((int)fuelCost);
            bubbleSpawner.SetSpawnInterval();
            fuelCost *= CostIncrease;
            fuelCost = Mathf.Round(fuelCost);
            UpdateCoin();

            fuelCostText.text = fuelCost.ToString();
        }
        else
        {
            Debug.Log("Not Enough cash");
        }
    }

    void CoinIncreaseButton()
    {
        if (GameManager.instance.CoinManager.Coins >= coinCost)
        {
            GameManager.instance.RemoveCoins((int)coinCost);
            coinSpawner.IncreaseSpawnRate();
            coinCost *= CostIncrease;
            coinCost = Mathf.Round(coinCost);
            coinCostText.text = coinCost.ToString();
            UpdateCoin();

        }
        else
        {
            Debug.Log("Not Enough cash");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCoin()
    {
        coins.text = GameManager.instance.CoinManager.Coins.ToString();
    }
    private void OnEnable()
    {
        coins.text =  GameManager.instance.CoinManager.Coins.ToString();
    }
}
