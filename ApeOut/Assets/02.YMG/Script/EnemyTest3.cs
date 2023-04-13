using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyTest3 : MonoBehaviour
{
    Transform Target;
    NavMeshAgent Agent;
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
        spawnRate = AttackAnime.length;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (state == State.Idle)
        {
            Debug.Log("대기 상태");
            Idle();
        }
        else if (state == State.Move)
        {
            Debug.Log("이동 상태");
            Move();
        }
        else if (state == State.Engage)
        {
            Debug.Log("공격 상태");
            Engage();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void FaceTarget()
    {
        // direction to the target
        Vector3 direction = (Target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

    }       // FaceTarget()

    void Shot()
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

    void Engage() // 공격 상태
    {
        Agent.speed = 0;

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance > attackRange) // 공격 거리를 벗어나면
        {
            state = State.Move; // 이동 상태
            Anim.SetTrigger("MoveTrg");
        }

        // Attack motion 재생 중일 때는 FaceTarget() 을 무시하고 싶음.
        FaceTarget();
        Shot();
    }

    void Move() // 이동 상태
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= attackRange) // 공격 거리 이내라면
        {
            state = State.Engage; // 공격 상태
            Anim.SetTrigger("AttackTrg");
        }

        //타겟 방향으로 이동하다가
        Agent.speed = speed;
        //요원에게 목적지를 알려준다.
        Agent.SetDestination(Target.position);
    }

    void Idle() // 대기 상태
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        Agent.speed = 0;

        if (distance <= lookRange) // 감지거리 이내라면
        {
            state = State.Move; // 이동 상태
            Anim.SetTrigger("MoveTrg");
        }

    }

}
