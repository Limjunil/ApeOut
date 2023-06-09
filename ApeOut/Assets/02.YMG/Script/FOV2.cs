using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV2 : MonoBehaviour // 시야 범위 시각화
{
    public Transform player;
    public float maxAngle;
    public float maxRadius;

    private bool isInFOV = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        // 시야 각도
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (!isInFOV)
        {
            // 플레이어와의 거리
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        // 오브젝트의 정면
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);

    }

    public static bool InFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius) 
    {
        Collider[] overlaps = new Collider[10];
        // Overlap중첩 Sphere구면 Non Allocate비 할당
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for(int i = 0; i < count +1; i++) 
        {
            if (overlaps[i] != null) 
            {
                if (overlaps[i].transform == target) 
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle) 
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) 
                        {
                            if (hit.transform == target)
                                return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void Update()
    {
        isInFOV = InFOV(transform, player, maxAngle, maxRadius);
    }

}
