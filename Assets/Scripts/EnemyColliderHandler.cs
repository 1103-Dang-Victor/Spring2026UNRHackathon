using UnityEngine;
using System.Collections.Generic;

public class EnemyColliderHandler : MonoBehaviour
{
    private HashSet<EnemyStateHandler> hitEnemies = new HashSet<EnemyStateHandler>();
    private void OnEnable()
    {
        hitEnemies.Clear(); // reset every attack
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit: " + other.name);

        EnemyStateHandler enemy = other.GetComponentInParent<EnemyStateHandler>();

        if (enemy != null && !hitEnemies.Contains(enemy))
        {
            hitEnemies.Add(enemy);

            Debug.Log("Damage Taken!");
            enemy.DamageTaken(25);
        }
    }
}
