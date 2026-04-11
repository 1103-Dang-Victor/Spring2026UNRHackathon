using System;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private int currentShield;
    private int maxShield;
    private int currentDamage;
    public static event Action<int, int> OnHealthChanged;

    void Start()
    {
        currentHealth = 100;
        maxHealth = 100;
        currentShield = 0;
        maxShield = 20;
        currentDamage = 2;
    }

/*
    public void GrabbedMaxHealthPowerUp() {
        Debug.Log("touch");
        // Invoke the event if there are any subscribers
        MaxHealthPowerUp?.Invoke();
    }
*/
    void OnEnable()
    {
        PowerUpHandler.MaxHealthPowerUp += MaxHealthPowerUpGrabbed;
        PowerUpHandler.CurrentHealthPowerUp += CurrentHealthPowerUpGrabbed;
        PowerUpHandler.DamagePowerUp += DamagePowerUpGrabbed;
    }

    void OnDisable()
    {
        PowerUpHandler.MaxHealthPowerUp -= MaxHealthPowerUpGrabbed;
        PowerUpHandler.CurrentHealthPowerUp -= CurrentHealthPowerUpGrabbed;
        PowerUpHandler.DamagePowerUp -= DamagePowerUpGrabbed;
    }

    void MaxHealthPowerUpGrabbed(int statBonus)
    {
        Debug.Log("PowerUp event received!");
        Debug.Log(maxHealth);
        updateMaxHealth(statBonus); // your private method
        Debug.Log(maxHealth);
    }

    void CurrentHealthPowerUpGrabbed(int statBonus)
    {
        Debug.Log("PowerUp event received!");
        Debug.Log(currentHealth);
        addToCurrentHealth(statBonus); // your private method
        Debug.Log(currentHealth);
    }

    void DamagePowerUpGrabbed(int statBonus)
    {
        Debug.Log("PowerUp event received!");
        Debug.Log(currentDamage);
        addToCurrentDamage(statBonus); // your private method
        Debug.Log(currentDamage);
    }

    private void addToCurrentHealth(int newValue)
    {
        currentHealth += newValue;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void subtractFromCurrentHealth(int newValue)
    {
        currentHealth -= newValue;
    }

    private void updateMaxHealth(int newValue)
    {
        maxHealth += newValue;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void subtractMaxHealth(int newValue)
    {
        maxHealth -= newValue;
    }

    private void addToCurrentDamage(int newValue)
    {
        currentDamage += newValue;
    }

    private void subtractFromCurrentDamage(int newValue)
    {
        currentDamage -= newValue;
    }
}