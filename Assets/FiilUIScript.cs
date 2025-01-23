using UnityEngine;
using UnityEngine.UI;
public class FiilUIScript : MonoBehaviour
{
    [SerializeField] Image fillFuel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateFill();
        
    }
    void CalculateFill()
    {
        fillFuel.fillAmount = GameManager.instance.PlayerController.fuel.Fuel / GameManager.instance.PlayerController.fuel.MaxFuel;
    }
}
