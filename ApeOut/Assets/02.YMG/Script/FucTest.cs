using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FucTest : MonoBehaviour
{
    NavMeshAgent agent;
    // array for waypoints
    public Transform[] waypoints;
    // index for choosing them
    int waypointIndex;
    Vector3 target;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        // if our distance to our target is less than one meter
        if (Vector3.Distance(transform.position, target) < 1) 
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination() 
    {
        // set target to our current waypoint
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    // going to increase the waypoint index by one
    void IterateWaypointIndex() 
    {
        waypointIndex++;
        // if the waypoint index is equal to the number of waypoints we have
        if (waypointIndex == waypoints.Length) 
        {
            // set it back to zero
            waypointIndex = 0;
        }
    }
}
