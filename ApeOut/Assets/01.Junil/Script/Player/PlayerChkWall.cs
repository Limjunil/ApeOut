using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChkWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            //isWall = true;
            GFunc.Log($"{other.transform.position}");

            //gameObject.transform.position = collision.transform.position;
        }
    }
}
