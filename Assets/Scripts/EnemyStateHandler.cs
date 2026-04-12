using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    private int currentHealth;
   private int maxHealth;
   private int currentShield;
   private int maxShield;
   private int currentDamage;
   private bool dead = false;
   //public static event Action<int> OnTakenDamageFromPlayer;
  
   void Update() {
       if (dead)
       {
           Destroy(gameObject);
       }
   }


   void Start()
   {
       currentHealth = 100;
       maxHealth = 100;
       currentShield = 0;
       maxShield = 20;
       currentDamage = 2;
   }


   void OnEnable()
   {
       //somethinghandler.EventName += DamageTaken;
   }


   void OnDisable()
   {
       //somethinghandler.EventName -= DamageTaken;
   }


   public void DamageTaken(int damage)
   {
       Debug.Log("damage event received");
       //Debug.Log(currentHealth);
       subtractFromCurrentHealth(damage);
       Debug.Log($"this enemy's health is now: {currentHealth}");
   }


   private void addToCurrentHealth(int newValue)
   {
       currentHealth += newValue;
   }


   private void subtractFromCurrentHealth(int newValue)
   {
       currentHealth -= newValue;
       if (currentHealth <= 0)
       {
           dead = true;
       }
   }


   private void updateMaxHealth(int newValue)
   {
       maxHealth += newValue;
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
