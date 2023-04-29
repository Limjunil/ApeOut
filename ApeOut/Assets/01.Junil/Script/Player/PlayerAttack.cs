using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 공격 콜라이더 게임 오브젝트 캐싱
    public BoxCollider attackCollider = default;

    // 공격 중인지 확인하는 bool 값
    public bool isAttackChk = false;

    // 잡고 있는 중인지 확인하는 bool 값
    public bool isGrabChk = false;

    private void Awake()
    {
        attackCollider = gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>();

        attackCollider.enabled = false;

        isAttackChk = false;
        isGrabChk = false;
    }


    //! 플레이어 공격 함수
    public void OnAttack()
    {
        StartCoroutine(OnOffAtkCol());
    }

    //! 플레이어가 적을 잡는 함수
    public void OnHold()
    {
        StartCoroutine(OnOffHoldCol());
    }

    //! 플레이어가 적을 놓아주는 함수
    public void OffHold()
    {
        isGrabChk = false;
    }

    // 공격할 때 일정 시간 후 공격 콜라이더를 켰다가 끄는 코루틴
    IEnumerator OnOffAtkCol()
    {
        
        isAttackChk = true;

        yield return new WaitForSeconds(0.3f);

        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);

        isAttackChk = false;


    }

    // 공격할 때 일정 시간 후 공격 콜라이더를 켰다가 끄는 코루틴
    IEnumerator OnOffHoldCol()
    {
        isGrabChk = true;

        yield return new WaitForSeconds(0.3f);

        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackCollider.enabled = false;



    }



}
