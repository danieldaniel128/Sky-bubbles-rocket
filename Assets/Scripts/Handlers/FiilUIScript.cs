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
    public List<Image> images = new List<Image>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.PlayerController.onHit += RemoveHeart;
    }

    // Update is called once per frame
    void Update()
    {
        score += addition * Time.deltaTime;
        CalculateFill();
        MetersText.text = "Meters Passed: " + (int)score;


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
        Image image = images[GameManager.instance.PlayerController.lives];
        image.enabled = false;
    }
}
