using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public GameObject Door;

    public float opening = 10f;
    public float closing = 0f;
    public float moveSpeed = 5f;

    bool isPlayer;
    //bool isOpen;

    void Start()
    {
        isPlayer = false;
        //isOpen = false;
    }

    void Update()
    {
        if (isPlayer)
        {
            if (Door.transform.position.x < opening)
            {
                Door.transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
            }
        }
        else 
        {
            if (Door.transform.position.x > closing)
            {
                Door.transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            isPlayer = false;
        }
    }
}
