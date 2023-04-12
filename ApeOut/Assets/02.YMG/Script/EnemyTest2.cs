using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class EnemyTest2 : MonoBehaviour
{
    public AnimationClip SpawnAnime;
    public AnimationClip AttackAnime;

    public float LookRange = 20f;
    public float CheckRange = default;
    public float AttackRange = default;

    public bool headAttack = false;
    public bool Charging = false;
    public Transform spawnPoint;

    public GameObject bulletPrefab;
    private float spawnRate; // 생성 주기
    private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간

    NavMeshAgent _pathFinder;
    Transform _player; // 목표
    Animator _anim;

    public float Speed;
    private bool BeAttackedEnd;

    enum State
    {
        Idle,
        Move,
        Check,
        Attack,
        BeAttacked
    }

    State state;

    private void Awake()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        spawnRate = AttackAnime.length;

        LookRange = 20f;
        //Speed = 5f;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimeWaiting());
        state = State.Idle;
    }

    void Update()
    {
        if (_anim.GetBool("SpawnEnd") && _pathFinder.enabled == true)
        {
            if (state == State.Idle)
            {
                Debug.Log("대기 상태");
                UpdateIdle();
            }
            else if (state == State.Move)
            {
                Debug.Log("이동 상태");
                UpdateRun();
            }
            else if (state == State.Check)
            {
                Debug.Log("견제 상태");
                UpdateCheck();
            }
            else if (state == State.Attack)
            {
                Debug.Log("공격 상태");
                UpdateAttack();
            }
            else if (state == State.BeAttacked)
            {
                Debug.Log("공격 받는 중");
                UpdateBeAttacked();
            }
        }
    }

    IEnumerator AnimeWaiting()
    {
        yield return new WaitForSeconds(SpawnAnime.length);
        // 애니메이션 발동 조건 
        _anim.SetBool("SpawnEnd", true);
        Debug.Log("시작 코루틴");
    }

    private void UpdateAttack()
    {
        Charging = true;
        _pathFinder.speed = 0;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > AttackRange)
        {
            state = State.Check;
            _anim.SetTrigger("isCheckTrg");
        }

        // Attack motion 재생 중일 때는 FaceTarget() 을 무시하고 싶음.
        FaceTarget();
    }

    private void UpdateCheck()
    {
        _pathFinder.speed = 0;
        Charging = false;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= AttackRange)
        {
            state = State.Attack;
            _anim.SetTrigger("isAttackTrg");
        }

        if (distance > CheckRange)
        {
            state = State.Move;
            _anim.SetTrigger("isMoveTrg");
        }

        FaceTarget();
        Fireball();
    }

    private void UpdateRun()
    {
        Charging = true;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= CheckRange)
        {
            state = State.Check;
            _anim.SetTrigger("isCheckTrg");
        }

        //타겟 방향으로 이동하다가
        _pathFinder.speed = Speed;
        //요원에게 목적지를 알려준다.
        _pathFinder.SetDestination(_player.position);
    }

    void UpdateIdle()
    {
        Charging = true;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        _pathFinder.speed = 0;

        if (distance <= LookRange)
        {
            state = State.Move;
            _anim.SetTrigger("isMoveTrg");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, LookRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CheckRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    void FaceTarget()
    {
        if (headAttack == false)
        {
            // direction to the target
            Vector3 direction = (_player.position - transform.position).normalized;
            // rotation where we point to that target
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            // update our own rotation to point in this direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);
        }
    }       // FaceTarget()

    void Fireball()
    {
        if (Charging == false)
        {
            // timeAfterSpawn 갱신
            timeAfterSpawn += Time.deltaTime;

            // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
            if (timeAfterSpawn >= spawnRate)
            {
                // 누적된 시간을 리셋
                timeAfterSpawn = 0f;
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                bullet.transform.LookAt(_player);
                spawnRate = AttackAnime.length;
            }
        }
    }

    void UpdateBeAttacked()
    {
        _pathFinder.speed = 0;

        if (BeAttackedEnd == true)
        {
            state = State.Move;
            _anim.SetTrigger("isMoveTrg");
        }
    }

    public void OnDamaged(int damage)
    {
        BeAttackedEnd = false;

        if (state == State.Idle || state == State.Move || state == State.BeAttacked)
        {
            state = State.BeAttacked;
            _anim.SetTrigger("BeAttackedTrg");
        }
    }
}
