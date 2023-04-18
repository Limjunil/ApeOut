using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    public GameObject explosionEffect;
    bool hasExploded = false;

    public int enemyHp = default;
    public int enemyAtk = default;

    public Transform Target;
    public NavMeshAgent Agent;

    public LayerMask PlayerMask;
    public LayerMask ObstacleMask;

    public float viewAngle; // 감지 범위
    public float maxAngle; // 범위 기즈모
    public float maxRadius; // 감지 거리

    public bool isInFOV = false;
    public bool isAim = false;
    public bool isEnemy = false;

    //

    public Animator Anim;
    public AnimationClip AttackAnime;
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    public float timeAfterSpawn; // 최근 생성 시점에서 지난 시간
    public float lookRange = default; // 감지 거리
    public float attackRange = default; // 공격 거리
    public float enemySpeed = 3;
    public float spawnRate;


    public enum State
    {
        Guard,
        Action,
        Engage,
        Move
    }

    public State state;

    protected virtual void Awake()
    {
        //부모에서 작동할 내용
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public virtual void Move() //! 시야 밖 상태
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        //Debug.Log($"Move() {Agent.speed}");

        if (isEnemy == true)
        {
            isAim = false;
            //Debug.Log($"Move() == true {Agent.speed}");
            Agent.speed = enemySpeed;

            Agent.SetDestination(Target.position);

        }

        if (distance <= maxRadius)
        {
            FaceTarget();
            state = State.Action;
        }

    }

    public virtual void Range() //! 시야 범위 안 내비메시
    {
        isAim = false;

        Agent.speed = 0;
        Agent.SetDestination(Target.position);
        //Debug.Log($"Range() {Agent.speed}");

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
                    //Debug.Log($"Range() in angle {Agent.speed}");
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

    public virtual void Engage() //! 공격 상태
    {
        //Debug.Log($"Engage() {isAim}");
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

    public virtual void Guard() //! 경계
    {
        isAim = false;

        Agent.speed = 0;
        Agent.SetDestination(Target.position);
        //Debug.Log($"Guard() {Agent.speed}");

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

    private void OnCollisionEnter(Collision collision) // Death
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Explosion
            if (!hasExploded)
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                hasExploded = true;
            }

            // Object
            Agent.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public virtual void FaceTarget() //! 바라보는 기능
    {
        // direction to the target
        Vector3 direction = (Target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemySpeed);

    }

    public virtual void Shot() //! 공격 기능
    {

        // timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;


    }


    public virtual void OnDrawGizmos() //! 기즈모
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



}
