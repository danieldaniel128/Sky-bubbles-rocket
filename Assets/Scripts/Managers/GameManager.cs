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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
