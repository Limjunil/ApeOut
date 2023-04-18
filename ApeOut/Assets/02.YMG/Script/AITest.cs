using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : EnemyBase
{

    protected override void Awake()
    {
        base.Awake();
        //자식에서 추가로 동작할 내용

        spawnRate = AttackAnime.length;
    }

    private void FixedUpdate()
    {
        isInFOV = InFOV(transform, Target, maxAngle, maxRadius);

        if (state == State.Guard)
        {
            Debug.Log("경계 상태");
            Guard();
        }
        else if (state == State.Action)
        {
            Debug.Log("행동 상태");
            Range();
        }
        else if (state == State.Engage)
        {
            Debug.Log("공격 상태");
            Engage();
        }
        else if (state == State.Move)
        {
            Debug.Log("이동 상태");
            Move();
        }

    }


    public override void Shot()
    {
        base.Shot();

        // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            // 누적된 시간을 리셋
            timeAfterSpawn = 0f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            bullet.transform.LookAt(Target);
            spawnRate = AttackAnime.length;
        }
    }

    public static bool InFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius) //! 기즈모 레이
    {
        Collider[] overlaps = new Collider[20];
        // Overlap중첩 Sphere구면 Non Allocate비 할당
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
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

}
