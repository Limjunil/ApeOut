using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public Transform[] waypoints;
    // index for choosing them
    public int waypointIndex;
    public Vector3 targetVector3;

    public GameObject explosionEffect;
    bool isHasExploded = false;

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

    public bool isHold = false;
    public bool isHitPlayer = false;

    public Rigidbody enemyRigid = default;

    public LineRenderer lR;

    public enum State
    {
        Guard,
        Action,
        Engage,
        Move,
        Patrol,
        hold
    }

    public State state;

    public virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        //부모에서 작동할 내용
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        //Target = GameObject.FindGameObjectWithTag("Player").transform;
        Target = PlayerManager.Instance.player.transform;

        UpdateDestination();

        lR = GetComponent<LineRenderer>();
    }

    public virtual void HoldToPlayer()
    {
        isHold = true;

        state = State.hold;
    }

    // [Junil, YMG] 적이 플레이어에게 잡히는 함수
    public virtual void HoldEnemy()
    {
        gameObject.transform.position = PlayerManager.Instance.player.holdEnemyPos.transform.position;

        if(PlayerManager.Instance.player.playerAttack.isGrabChk == false)
        {
            isHold = false;

            state = State.Guard;
        }
    }

    public virtual void Patrol()
    {
            Anim.SetTrigger("MoveTrg");
        // if our distance to our target is less than one meter
        if (Vector3.Distance(transform.position, targetVector3) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        if (isInFOV)
        {
            //Debug.Log($"Patrol{isInFOV}");
            state = State.Action;
        }

        float distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance <= lookRange) // 탐지거리 이내라면
        {
            FaceTarget();
            state = State.Action;
        }
    }


    void UpdateDestination()
    {
        // set target to our current waypoint
        targetVector3 = waypoints[waypointIndex].position;
        Agent.SetDestination(targetVector3);
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
        //Agent.velocity = Vector3.zero;
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

        //if (state != State.Engage)
        //{

        //    lR.enabled = false;
        //}

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

    public virtual void OnCollisionEnter(Collision collision) // Death
    {
        if (collision.collider.CompareTag(RDefine.PLAYER_TAG)) 
        {
            Debug.Log("Player");
            enemyRigid.velocity = Vector3.zero;
            enemyRigid.isKinematic = true;
            enemyRigid.isKinematic = false;
        }

        if (collision.collider.CompareTag("Wall") && isHitPlayer == true)
        {
            // Explosion
            if (!isHasExploded)
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                isHasExploded = true;
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

    public virtual void StopForce()
    {
        StartCoroutine(StopEnemy());
    }

    IEnumerator StopEnemy()
    {
        isHitPlayer = true;
        yield return new WaitForSeconds(1f);
        enemyRigid.velocity = Vector3.zero;
        isHitPlayer = false;

    }


}
