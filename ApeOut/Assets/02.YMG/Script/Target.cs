using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(health);
    }

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }

    void Die() 
    {
        gameObject.SetActive(false);
    }
}
