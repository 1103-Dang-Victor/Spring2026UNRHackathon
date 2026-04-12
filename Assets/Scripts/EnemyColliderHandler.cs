using UnityEngine;
using System.Collections.Generic;

public class EnemyColliderHandler : MonoBehaviour
{
    private HashSet<EnemyStateHandler> hitEnemies = new HashSet<EnemyStateHandler>();
    public PlayerStatHandler playerStats;


    private void OnEnable()
    {
        hitEnemies.Clear(); // reset every attack
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit: " + other.name);

        EnemyStateHandler enemy = other.GetComponentInParent<EnemyStateHandler>();

        PlayerStatHandler player = other.GetComponentInParent<PlayerStatHandler>();

        if (playerStats == null)
        {
            Debug.LogWarning("No PlayerStatHandler found on object or parents!");
            return;
        }

        int damage = playerStats.currentDamage;
        
        if (enemy != null && !hitEnemies.Contains(enemy))
        {
            hitEnemies.Add(enemy);

            Debug.Log("Damage Taken!");
            enemy.DamageTaken(damage);
        }
    }
}
