using UnityEngine;

public class FuelScript : MonoBehaviour
{
    [SerializeField] float fuel = 100f;
    [SerializeField] float fuelConsumption = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuel -= fuelConsumption * Time.deltaTime;
    }
    
    public void Refuel(float amount)
    {
        fuel += amount;
    }
}
