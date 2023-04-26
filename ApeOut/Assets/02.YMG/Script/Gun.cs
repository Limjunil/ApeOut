using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f; // 데미지
    public float range = 50f;

    public GameObject muzzle;

    void Start()
    {
        //if (Input.GetButtonDown(""))
        //{
        //}
    }

    void Update()
    {
            Shoot();
        
    }

    void Shoot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.collider.name);
            //Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * range, Color.red);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos() 
    {
        // 오브젝트의 정면
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * range);
    }

}
