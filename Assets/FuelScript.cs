using UnityEngine;

public class FuelScript : MonoBehaviour
{
    [SerializeField] float fuel = 100f;
    [SerializeField] float fuelConsumption = 5f;
    public float Fuel { get { return fuel; } }
    public float MaxFuel { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxFuel = fuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (fuel>MaxFuel)
        {
            fuel = MaxFuel;
        }
        fuel -= fuelConsumption * Time.deltaTime;
        if (fuel <= 0)
        {
            print("Out of fuel");
            GameManager.instance.LoseGame();
        }
    }
    
    public void Refuel(float amount)
    {
        fuel += amount;
    }
    public void Setfuel(float amount)
    {
        fuel = amount;
        MaxFuel = fuel;
    }
}
