using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3f;
    private float countdown;
    private bool hasExploded = false;
    
    public GameObject explosionEffect;
    public GameObject explosionArea;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) 
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode() 
    {
        Debug.Log("BOOM");
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
