using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int coins;
    public int Coins => coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void RemoveCoins(int amount)
    {
        coins -= amount;
        if (coins <= 0)
        {
            coins = 0;
        }
    }
}
