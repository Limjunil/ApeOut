using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prototype2 : MonoBehaviour
{
    Transform Target;
    NavMeshAgent Agent;

    public LayerMask PlayerMask;
    public LayerMask ObstacleMask;

    public float viewAngle; // 감지 범위
    public float maxAngle; // 범위 기즈모
    public float maxRadius; // 감지 거리

    private bool isInFOV = false;
    private bool isAim = false;

    //

    Animator Anim;
    public AnimationClip AttackAnime;
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간
    public float lookRange = default; // 감지 거리
    public float attackRange = default; // 공격 거리
    public float speed = 3;
    private float spawnRate;

    enum State
    {
        Idle,
        Move,
        Engage
    }

    State state;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        spawnRate = AttackAnime.length;
    }

    private void FixedUpdate()
    {
        isInFOV = InFOV(transform, Target, maxAngle, maxRadius);

        if (state == State.Idle)
        {
            Debug.Log("대기 상태");
            //Idle();
            Range();
            Guard();
        }
        else if (state == State.Move)
        {
            Debug.Log("이동 상태");
            Range();
        }
        else if (state == State.Engage)
        {
            Debug.Log("공격 상태");
            Engage();
        }

    }

    void Idle() //! 대기 상태
    {
        isAim = false;

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        Agent.speed = 0;

        if (distance <= lookRange) // 감지거리 이내라면
        {
            state = State.Move; // 이동 상태
            Anim.SetTrigger("MoveTrg");
        }

    }

    void Range() //! 시야 범위 안 내비메시
    {
        isAim = false;

        Vector3 playerTarget = (Target.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
            if (distanceToTarget <= maxRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, ObstacleMask) == false)
                {
                    state = State.Move;
                    Debug.Log("player");
                    Agent.SetDestination(Target.position);
                    Anim.SetTrigger("MoveTrg");

                }
            }
        }

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= attackRange) // 공격 거리 이내라면
        {
            state = State.Engage; // 공격 상태
            
        }

        //타겟 방향으로 이동하다가
        Agent.speed = speed;

    }

    void Engage() //! 공격 상태
    {
        isAim= true;

        Agent.speed = 0;
        Anim.SetTrigger("AttackTrg");

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (isAim == true)
        {
            // Attack motion 재생 중일 때는 FaceTarget() 을 무시하고 싶음.
            FaceTarget();
            Shot();
        }

        if (distance > attackRange) // 공격 거리를 벗어나면
        {
            isAim = false;
            state = State.Move; // 이동 상태
            
        }

    }

    void FaceTarget() //! 바라보는 기능
    {
        // direction to the target
        Vector3 direction = (Target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

    }

    void Shot() //! 공격 기능
    {

        // timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;

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

    void Guard() //! 경계
    {
        isAim = false;
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        Agent.speed = 0;
        if (distance <= lookRange) // 탐지거리 이내라면
        {
            FaceTarget();

        }
    }


    private void OnDrawGizmos() //! 기즈모
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

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
        if (Target != null)
        {
            Gizmos.DrawRay(transform.position, (Target.position - transform.position).normalized * maxRadius);
        }

        // 오브젝트의 정면
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);

    }

    public static bool InFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius) //! 기즈모 레이
    {
        Collider[] overlaps = new Collider[10];
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