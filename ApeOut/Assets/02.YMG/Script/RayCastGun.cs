using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastGun : MonoBehaviour
{
    //public GameObject laserto;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    LineRenderer laserLine;
    float fireTimer;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate) // laserLine != null
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = transform.forward * gunRange;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, laserOrigin.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject);
                Debug.Log("Destroy");
            }
            else 
            {
                laserLine.SetPosition(1, rayOrigin + (laserOrigin.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser() 
    {
        laserLine.enabled= true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
