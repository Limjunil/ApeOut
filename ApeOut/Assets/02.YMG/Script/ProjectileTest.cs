using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    private Rigidbody bulletRigidbody;

    private void OnDisable()
    {
        StopBulletOff();
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        StartBulletOff();
    }

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == RDefine.PLAYER_TAG)
        {
            PlayerManager.Instance.player.DamagePlayer();
        }
    }


    // [Junil, YMG] 총알이 일정 시간 후 사라지는 코루틴
    IEnumerator BulletOff = default;

    public void StartBulletOff()
    {
        BulletOff = AutoBulletOff();
        StartCoroutine(BulletOff);
    }

    public void StopBulletOff()
    {
        if(BulletOff != null)
        {
            StopCoroutine(BulletOff);
        }
    }


    public IEnumerator AutoBulletOff()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

}
