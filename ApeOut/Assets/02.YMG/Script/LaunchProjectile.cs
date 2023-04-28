using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchVelocity = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            // 생성 오브젝트, 발사 위치
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            // 생성 정면 * 힘
            _projectile.GetComponent<Rigidbody>().velocity = launchPoint.forward * launchVelocity;
        }
    }
}
