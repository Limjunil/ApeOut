using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float time = 5f;
    public float zero = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(time);
        Timer();
    }

    void Awake()
    {
    }

    void Timer() 
    {
        time -= 1 * Time.deltaTime;
        if (time <= zero)
        {
            Debug.Log("zero");
            Destroy(gameObject);
        }
    }
}
