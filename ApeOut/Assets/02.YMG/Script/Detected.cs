using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    bool isDetected;
    GameObject target;
    public Transform enemy;

    void Start()
    {
        
    }

    void Update()
    {
        if (isDetected) 
        {
            enemy.LookAt(target.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            isDetected = true;
            target = other.gameObject;
        }
    }
}
