using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 공격 콜라이더 게임 오브젝트 캐싱
    public BoxCollider attackCollider = default;

    // 공격 중인지 확인하는 bool 값
    public bool isAttack = false;

    private void Awake()
    {
        attackCollider = gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>();

        attackCollider.enabled = false;
        isAttack = false;
    }


    //! 플레이어 공격 함수
    public void OnAttack()
    {
        GFunc.Log("곰 공격!");

        StartCoroutine(OnOffAtkCol());
    }

    // 공격할 때 일정 시간 후 공격 콜라이더를 켰다가 끄는 코루틴
    IEnumerator OnOffAtkCol()
    {
        isAttack = true;

        yield return new WaitForSeconds(0.3f);

        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);

        isAttack = false;


    }
}
