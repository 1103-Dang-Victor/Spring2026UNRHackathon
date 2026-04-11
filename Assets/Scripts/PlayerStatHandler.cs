using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private int currentShield;
    private int maxShield;
    

    void Start()
    {
        currentHealth = 100;
        maxHealth = 100;
        currentShield = 0;
        maxShield = 20;
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    private void addToCurrentHealth(int newValue)
    {
        currentHealth += newValue;
    }

    private void subtractFromCurrentHealth(int newValue)
    {
        currentHealth -= newValue;
    }

    private void updateMaxHealth(int newValue)
    {
        maxHealth += newValue;
    }

    private void subtractMaxHealth(int newValue)
    {
        maxHealth -= newValue;
    }


}
