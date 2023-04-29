using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomer : EnemyBase
{
    public int enemyBulleyVal = 30;
    public GameObject[] enemyBulletPack = default;

    public int chkBullet = 0;

    public Transform startPoint; //

    public float time = 5f;
    public float zero = 0;
    public float launchVelocity = 5f; // 투척 힘
    public GameObject projectile; // 던질 폭탄
    public GameObject deathBomb;

    private bool isbomb = true; // 폭탄 관리

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        isHitPlayer = false;

        spawnRate = AttackAnime.length;
        enemyBulleyVal = 30;
        enemyBulletPack = new GameObject[enemyBulleyVal];
        chkBullet = 0;

        enemyRigid = gameObject.GetComponent<Rigidbody>();

        for (int i = 0; i < enemyBulleyVal; i++)
        {
            enemyBulletPack[i] = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            enemyBulletPack[i].SetActive(false);
        }
    }

    public override void Awake()
    {
        base.Awake();
        //자식에서 추가로 동작할 내용

        //spawnRate = AttackAnime.length;
        //enemyBulleyVal = 30;
        //enemyBulletPack = new GameObject[enemyBulleyVal];
        //chkBullet = 0;

        //enemyRigid = gameObject.GetComponent<Rigidbody>();

        //for (int i = 0; i < enemyBulleyVal; i++)
        //{
        //    enemyBulletPack[i] = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        //    enemyBulletPack[i].SetActive(false);
        //}

    }

    private void FixedUpdate()
    {
        
        isInFOV = InFOV(transform, Target, maxAngle, maxRadius);

        if (state == State.Guard)
        {
            //Debug.Log("경계 상태");
            Guard();
        }
        else if (state == State.Action)
        {
            //Debug.Log("행동 상태");
            Range();
        }
        else if (state == State.Engage )
        {
            //Debug.Log("공격 상태");
            Engage();
        }
        else if (state == State.Move)
        {
            //Debug.Log("이동 상태");
            Move();
        }
        else if (state == State.Patrol)
        {
            //Debug.Log("순찰 상태");
            Patrol();
        }
        else if(state == State.hold)
        {
            //Debug.Log("홀드 상태");
            HoldEnemy();
        }

    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        Instantiate(deathBomb, transform.position, transform.rotation);
    }

    public override void Shot()
    {
        base.Shot();
        //Agent.velocity = Vector3.zero;

        // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            // 누적된 시간을 리셋
            timeAfterSpawn = 0f;
            //GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            enemyBulletPack[chkBullet].transform.localPosition = spawnPoint.transform.position;

            enemyBulletPack[chkBullet].transform.localRotation = spawnPoint.transform.rotation;

            //laserLine.SetPosition(0, laserOrigin.position);
            //laserLine.SetPosition(1, transform.forward * 20 + transform.position);

            //enemyBulletPack[chkBullet].transform.LookAt(Target);
            enemyBulletPack[chkBullet].transform.LookAt(Target);


            enemyBulletPack[chkBullet].SetActive(true);

            spawnRate = AttackAnime.length;

            chkBullet++;

            if (enemyBulleyVal <= chkBullet)
            {
                chkBullet = 0;
            }
        }

        //! 일정 시간이 지나면 폭탄 투척
        time -= 1 * Time.deltaTime;

        if (time <= zero)
        {
            if (isbomb) // 폭탄은 1개
            {
                isbomb = false;
                // 생성 오브젝트, 발사 위치
                var _projectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                // 생성 정면 * 힘
                _projectile.GetComponent<Rigidbody>().velocity = spawnPoint.forward * launchVelocity;
            }
        }

    }



    public static bool InFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius) //! 기즈모 레이
    {
        Collider[] overlaps = new Collider[100];
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