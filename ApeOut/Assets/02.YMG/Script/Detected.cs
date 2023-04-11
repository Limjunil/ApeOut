using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    bool isDetected;
    GameObject target;
    public Transform enemy;

    public GameObject bullet;
    public Transform shootPoint;

    public float shootSpeed = 10f;
    public float timeToShoot = 1.3f;
    float originalTime;

    void Start()
    {
        originalTime = timeToShoot;
    }

    void Update()
    {
        if (isDetected) 
        {
            enemy.LookAt(target.transform);
        }
    }

    private void FixedUpdate()
    {
        if (isDetected) 
        {
            timeToShoot -= Time.deltaTime;
            if (timeToShoot < 0) 
            {
                ShootPlayer();
                timeToShoot = originalTime;
            }
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

    private void ShootPlayer() 
    {
        GameObject currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rigidbody = currentBullet.GetComponent<Rigidbody>();

        rigidbody.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
    }
}
