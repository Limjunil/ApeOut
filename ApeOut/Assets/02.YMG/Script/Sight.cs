using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sight : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask PlayerMask;
    public LayerMask ObstacleMask;

    //public GameObject player;
    Transform Target;
    NavMeshAgent Agent;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 playerTarget = (Target.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
            if (distanceToTarget <= viewRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, ObstacleMask) == false)
                {
                    Debug.Log("player");
                    Agent.SetDestination(Target.position);

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
