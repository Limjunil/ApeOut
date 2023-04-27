using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAtkCol : MonoBehaviour
{
    private const float PUNCH_FORCE = 20f;

    //! 공격 콜라이더에 충돌하면 발생
    public void OnTriggerEnter(Collider other)
    {
        // 적이 충돌한 경우 발동하는 조건
        if(other.tag == RDefine.ENEMY_TAG)
        {
            if(PlayerManager.Instance.player.playerAttack.isAttackChk == true)
            {
                GFunc.Log("적 맞았음!");

                PlayerManager.Instance.mainCamera.isShake = true;

                Rigidbody tempEnemyRigid_ = other.gameObject.GetComponent<Rigidbody>();

                tempEnemyRigid_.AddForce(transform.forward * PUNCH_FORCE, ForceMode.Impulse);
            }

            if(PlayerManager.Instance.player.playerAttack.isGrabChk == true)
            {
                GFunc.Log("적 잡았음!");

                other.GetComponent<EnemyBase>().HoldToPlayer();
            }
        }
    }
}
