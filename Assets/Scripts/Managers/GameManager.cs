using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController PlayerController;
    public BackgroundScroller BackgroundScroller;
    public CoinManager CoinManager;
    public List<GameObject> activeEntity = new List<GameObject>();
    [SerializeField] GameObject InGameObjects;
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
    public void Launch()
    {
        InGameObjects.SetActive(true);
    }
    [ContextMenu("Lose Game")]
    public void LoseGame()
    {
        InGameObjects.SetActive(false);
        BackgroundScroller.DeactivateScrol();
    }
    public void AddCoins(int amount)
    {
        CoinManager.AddCoins(amount);
    }
}
