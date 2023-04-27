using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLaser : MonoBehaviour
{
    private LineRenderer lR;
    public Transform startPoint;


    void Start()
    {
        lR= GetComponent<LineRenderer>();
    }

    void Update()
    {
        lR.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(startPoint.position, transform.forward, out hit))
        {
            lR.SetPosition(1, hit.point);
            if (hit.collider)
            {
                lR.SetPosition(1, hit.point);
            }
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Player");
                //Destroy(hit.transform.gameObject);
            }
        }
        else { lR.SetPosition(1, transform.forward * 50); }

    }
    
}
