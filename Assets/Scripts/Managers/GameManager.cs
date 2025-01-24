using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController PlayerController;
    public CoinManager CoinManager;
    public List<GameObject> activeEntity = new List<GameObject>();
    public List<GameObject> activeBird = new List<GameObject>();
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
        foreach (var bird in activeBird)
        {
            Destroy(bird.gameObject);
        }

        activeEntity.Clear();
        activeBird.Clear();
        PlayerController.flashEffect.StopFlash();

        ScrapsSpawner.InitScrapsCoro();
    }
    public void Launch()
    {
        InGaqmeobjects.SetActive(true);
        isGameOver = false;
        
    }
    public void AddCoins(int amount)
    {
        CoinManager.AddCoins(amount);
    }
    public void RemoveCoins(int amount)
    {
        CoinManager.RemoveCoins(amount);
    }
}
