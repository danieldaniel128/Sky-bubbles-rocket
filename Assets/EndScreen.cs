using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Meterts;
    [SerializeField] TextMeshProUGUI Coins;
    [SerializeField] TextMeshProUGUI Bubbles;

    [SerializeField] Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.onClick.AddListener(RestTime);

        DisplayResult();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RestTime()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    private void DisplayResult()
    {
        
        Meterts.text = "Meters: " + GameManager.instance.metersPassed;
        Coins.text = "Coins: " + GameManager.instance.GetCoinsCollected().ToString();
        Bubbles.text = "Bubbles: " + GameManager.instance.GetBubblesPopped();

    }
}

