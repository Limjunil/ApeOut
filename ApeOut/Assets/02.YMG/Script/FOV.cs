using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FOV : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public float degree; // 각도

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // calculating the vector from the enemy Ai to the Player
        Vector3 direction = player.transform.position - transform.position;
        // [Vector3.Angle] it will give us the angle between two vectors
        if (Mathf.Abs(Vector3.Angle(transform.forward, direction)) < degree) 
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
