using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController PlayerController;
    public CoinManager CoinManager;
    public List<GameObject> activeEntity = new List<GameObject>();
    public bool isGameOver;
    [SerializeField] GameObject InGaqmeobjects;
    [SerializeField] BackgroundScroller BackgroundScroller;
    [SerializeField] ScrapsSpawner ScrapsSpawner;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    public void LoseGame()
    {
        InGaqmeobjects.SetActive(false);
        BackgroundScroller.DeactivateScrol();
        isGameOver = true;
        foreach (var entity in activeEntity)
        {
            Destroy(entity.gameObject);
        }
        
        activeEntity.Clear();
        ScrapsSpawner.InitScrapsCoro();
    }
    public void Launch()
    {
        InGaqmeobjects.SetActive(true);
    }
    public void AddCoins(int amount)
    {
        CoinManager.AddCoins(amount);
    }
}
