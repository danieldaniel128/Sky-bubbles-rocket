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
    int bubblesPopped = 0;
    int coinsCollected = 0;
    public int metersPassed = 0;
    [SerializeField] FiilUIScript FiilUIScript;
    [SerializeField]GameObject endPopup;
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
        Time.timeScale = 0;
        isGameOver = true;
        InGaqmeobjects.SetActive(false);
        endPopup.gameObject.SetActive(true);
        BackgroundScroller.DeactivateScrol();
       
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
        GetFinalScore();
        //FiilUIScript.RestMeters();
        //flashEffect.StopFlash();

        ScrapsSpawner.InitScrapsCoro();
    }
    public void Launch()
    {
        InGaqmeobjects.SetActive(true);
        isGameOver = false;
        bubblesPopped = 0;
        coinsCollected = 0;
        FiilUIScript.RestMeters();
        metersPassed = 0;

    }
    public void AddCoins(int amount)
    {
        CoinManager.AddCoins(amount);
    }
    public void RemoveCoins(int amount)
    {
        CoinManager.RemoveCoins(amount);
    }
    public void GetFinalScore()
    {
        metersPassed = (int)FiilUIScript.getScore();
        
    }
    public void AddBubblesPopped()
    {
        bubblesPopped++;
    }   
    public int GetBubblesPopped()
    {
        return bubblesPopped;
    }
    public void AddCoinsCollected()
    {
        coinsCollected++;
    }
    public int GetCoinsCollected()
    {
        return coinsCollected;
    }


}
