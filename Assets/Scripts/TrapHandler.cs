using UnityEngine;
using System;
using System.Collections.Generic;

public class TrapHandler : MonoBehaviour
{
    private int TrapDamage;
    public static event Action<int> DamageTaken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabbedDamagePowerUp(int damage) {
        Debug.Log("poked");
        // Invoke the event if there are any subscribers
        DamageTaken?.Invoke(TrapDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("SpikeTrap"))
        {
            TrapDamage = 3;
            Debug.Log("Hi");
            GrabbedDamagePowerUp(TrapDamage);
        }
    }
}
