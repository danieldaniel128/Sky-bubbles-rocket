using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class FiilUIScript : MonoBehaviour
{
    [SerializeField] Image fillFuel;
    [SerializeField] TextMeshProUGUI MetersText;
    [SerializeField] TextMeshProUGUI CoinsText;
    float score;
    [SerializeField] float addition;
    public List<GameObject> images = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.instance.PlayerController.onHit += RemoveHeart;
        GameManager.instance.PlayerController.onDeath += DisplayHearts;
    }
    private void OnEnable()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.instance.PlayerController.lives; i++)
        {
            images[i].SetActive(true);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        score += addition * Time.deltaTime;
        CalculateFill();
        MetersText.text = "Meters Passed: " + (int)score;
        CoinsText.text = "Coins: " + GameManager.instance.CoinManager.Coins;


    }
    void CalculateFill()
    {
        fillFuel.fillAmount = GameManager.instance.PlayerController.fuel.Fuel / GameManager.instance.PlayerController.fuel.MaxFuel;
    }
    void RemoveHeart()
    {
        if (GameManager.instance.PlayerController.lives< 0 )
        {
            return;
        }
        GameObject image = images[GameManager.instance.PlayerController.lives];
        image.SetActive(false);
    }
    private void DisplayHearts()
    {
        for (int i = 0; i < GameManager.instance.PlayerController.lives; i++)
        {
            images[i].SetActive(true);
        }
        CalculateFill();
    }
}
