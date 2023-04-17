using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prototype3 : MonoBehaviour
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
    private bool isEnemy = false;

    //

    Animator Anim;
    public AnimationClip AttackAnime;
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간
    public float lookRange = default; // 감지 거리
    public float attackRange = default; // 공격 거리
    public float enemySpeed = 3;
    private float spawnRate;

    enum State
    {
        Guard,
        Action,
        Engage,
        Move
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
        //state = State.Guard;
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

    void Move() //! 시야 밖 상태
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        Debug.Log($"Move() {Agent.speed}");

        if (isEnemy == true)
        {
            isAim = false;
            Debug.Log($"Move() == true {Agent.speed}");
            Agent.speed = enemySpeed;

            Agent.SetDestination(Target.position);

        }

        if (distance <= maxRadius)
        {
            FaceTarget();
            state = State.Action;
        }

    }

    void Range() //! 시야 범위 안 내비메시
    {
        isAim = false;

        Agent.speed = 0;
        Agent.SetDestination(Target.position);
        Debug.Log($"Range() {Agent.speed}");

        Vector3 playerTarget = (Target.transform.position - transform.position).normalized;

        // 두 벡터 사이의 거리
        float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

        // 두 벡터 사이의 각도가 viewAngle / 2 보다 작을 때
        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2) 
        {
            if (distanceToTarget <= maxRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, ObstacleMask) == false)
                {
                    Agent.speed = enemySpeed;
                    Debug.Log($"Range() in angle {Agent.speed}");
                    Anim.SetTrigger("MoveTrg");

                    isEnemy = true;
                    
                }
            }
        }

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= attackRange) // 공격 거리 이내라면
        {
            state = State.Engage; // 공격 상태
            isAim = true;
        }

        if (isEnemy == true) 
        {
            if (distanceToTarget >= maxRadius)
            {
                state = State.Move;
            }
        }

    }

    void Engage() //! 공격 상태
    {
        Debug.Log($"Engage() {isAim}");
        Agent.speed = 0;

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (isAim == true)
        {
            Anim.SetTrigger("AttackTrg");
            FaceTarget();
            Shot();
        }

        if (distance > attackRange) // 공격 거리를 벗어나면
        {
            isAim = false;
            state = State.Action; // 이동 상태
        }

    }

    void Guard() //! 경계
    {
        isAim = false;

        Agent.speed = 0;
        Agent.SetDestination(Target.position);
        Debug.Log($"Guard() {Agent.speed}");

        // 플레이어가 시야 밖 탐지거리에 들어오거나
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= lookRange) // 탐지거리 이내라면
        {
            FaceTarget();
            state = State.Action;
        }

        // 시야 내에 들어오면
        Vector3 playerTarget = (Target.transform.position - transform.position).normalized;

        // 두 벡터 사이의 각도가 viewAngle / 2 보다 작을 때
        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            // 두 벡터 사이의 거리
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

            // 가 maxRadius 보다 가까울 때
            if (distanceToTarget <= maxRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, ObstacleMask) == false)
                {
                    state = State.Action;
                    Anim.SetTrigger("MoveTrg");

                    if (distanceToTarget >= maxRadius)
                    {
                        isEnemy = true;
                        state = State.Move;
                    }
                }
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall")) 
        {
            Agent.enabled = false;
            gameObject.SetActive(false);
        }
    }

    void FaceTarget() //! 바라보는 기능
    {
        // direction to the target
        Vector3 direction = (Target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemySpeed);

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
