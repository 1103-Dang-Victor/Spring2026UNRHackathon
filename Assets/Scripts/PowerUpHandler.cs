using UnityEngine;
using System;
using System.Collections.Generic;


public class PowerUpHandler : MonoBehaviour
{
    
    private GameObject PowerUp;
    private PowerUpCharacteristics PowerUpStats;
    private PlayerStatHandler accessPlayerStats;

    public static event Action<int> MaxHealthPowerUp;
    public static event Action<int> DamagePowerUp;
    public static event Action<int> CurrentHealthPowerUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabbedMaxHealthPowerUp(int sb) {
        Debug.Log("touch");
        // Invoke the event if there are any subscribers
        MaxHealthPowerUp?.Invoke(PowerUpStats.statBonus);
    }
    
    public void GrabbedCurrentHealthPowerUp(int sb) {
        Debug.Log("touch");
        // Invoke the event if there are any subscribers
        CurrentHealthPowerUp?.Invoke(PowerUpStats.statBonus);
    }

    public void GrabbedDamagePowerUp(int sb) {
        Debug.Log("touch");
        // Invoke the event if there are any subscribers
        DamagePowerUp?.Invoke(PowerUpStats.statBonus);
    }

        // check what sprite it's using, and then classify based on that 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Powerup"))
        {
            PowerUp = collision.gameObject;
            PowerUpStats = PowerUp.GetComponent<PowerUpCharacteristics>();
            SpriteRenderer sr = PowerUp.GetComponent<SpriteRenderer>();
            string spriteTitle = sr.sprite.name;

            switch (spriteTitle) // get type of powerup based on sprite
            {
                case "gs_plus": // current health 
                    GrabbedCurrentHealthPowerUp(PowerUpStats.statBonus);
                    break;
                case "gs_star": // max health 
                    GrabbedMaxHealthPowerUp(PowerUpStats.statBonus);
                    break;
                case "gs_carat": // damage
                    GrabbedDamagePowerUp(PowerUpStats.statBonus);
                    break;
                default:
                    break;
            }
            Debug.Log("Current stat bonus:");
            Debug.Log(PowerUpStats.spriteTitle);
            Debug.Log(PowerUpStats.statBonus);
            Destroy(PowerUp);
        }
    }
}