
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public PlayerMovement playerMove = default;
    public Animator playerAnimator = default;
    public PlayerInput playerInput = default;
    public PlayerAttack playerAttack = default;

    public GameObject holdEnemyPos = default;

    public Image playerHpbar = default;

    public bool isFirstHold = false;

    // { [Junil] 플레이어 스탯
    public int playerHpMax = default;

    public int playerHp = default;

    public bool isDead = false;
    // } [Junil] 플레이어 스탯

    public GameObject playerHPUIObj = default; 


    private void Awake()
    {
        // 플레이어 싱글톤 호출
        PlayerManager.Instance.player = this;

        playerMove = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();
        holdEnemyPos = gameObject.transform.GetChild(3).gameObject;

        GameObject gameUIObjs_ = GFunc.GetRootObj("GameUIView");

        playerHPUIObj = gameUIObjs_.transform.GetChild(1).gameObject;

        playerHpbar = playerHPUIObj.transform.GetChild(0).gameObject.GetComponent<Image>();

        playerHp = 3;
        playerHpMax = playerHp;

        isDead = false;

        isFirstHold = false;


        playerAnimator.SetTrigger("OnIdle");
    }


    // Update is called once per frame
    void Update()
    {
        // 죽으면 정지
        if(isDead == true || UIManager.Instance.isOpenPause == true)
        {
            return;
        }

        ViewPlayerHp();

        LookMousePos();

        PlayerOnMove(playerInput.moveX, playerInput.moveZ);

        if (playerInput.isAttack == true)
        {
            if (playerAttack.isAttackChk == false)
            {
                if(playerAttack.isGrabChk == true)
                {
                    playerAttack.OffHold();
                }

                playerAttack.OnAttack();
                playerAnimator.SetTrigger("Attack1");
            }
        }


        if(playerInput.isGrab == true)
        {

            if (playerAttack.isGrabChk == false && isFirstHold == false)
            {
                playerAttack.OnHold();
                playerAnimator.SetTrigger("Attack3");


            }

            isFirstHold = true;
        }
        else if(playerInput.isGrab == false)
        {
            isFirstHold = false;

            if(playerAttack.isGrabChk == true)
            {
                playerAttack.OffHold();
            }
        }

        //GFunc.Log($"카메라 스크린 월드 : {Camera.main.WorldToScreenPoint(Input.mousePosition)}");
        //GFunc.Log($"플레이어 : {Camera.main.WorldToScreenPoint(gameObject.transform.position)}");

    }

    //! 플레이어 사망 시 호출
    public void PlayerDie()
    {
        // 플레이어가 죽게 되면 죽는 애니메이션과 죽는 액션의 카메라 호출
        playerAnimator.SetTrigger("isDead");

        PlayerManager.Instance.mainCamera.DeadCameraMove();

    }

    public void ViewPlayerHp()
    {

        float hpAmount_ = playerHp / (float)playerHpMax;

        playerHpbar.fillAmount = hpAmount_;

        if (playerHp <= 0 && isDead == false)
        {
            isDead = true;
            PlayerDie();
        }
    }   // ViewPlayerHp

    public void PlayerOnMove(float inputX_, float inputZ_)
    {
        if (inputX_ == 0 && inputZ_ == 0)
        {
            playerAnimator.SetBool("isRun", false);

        }
        else
        {
            playerAnimator.SetBool("isRun", true);
        }

        playerMove.OnMove(inputX_, inputZ_);
    }   // PlayerOnMove


    //! 곰이 마우스 커서 위치를 바라보는 함수
    public void LookMousePos()
    {

        var mousPosTest_ = Input.mousePosition;
        // 화면 상 좌표에 Z 값이 없기에, 월드 좌표로서의 Z 값을 정할 때 고려할 카메라와의 거리를 더해줘야 한다.
        mousPosTest_.z = Camera.main.nearClipPlane + 7;


        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(mousPosTest_);

        Vector3 playerPos_ = gameObject.transform.position;


        // 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector3 len_ = mousePos_ - playerPos_;

        float lookY_ = Mathf.Atan2(len_.x, len_.z);

        float rotateY_ = lookY_ * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, rotateY_, 0);


        playerAnimator.SetFloat("inputX", len_.x);
        playerAnimator.SetFloat("inputZ", len_.z);



    }   // LookMousePos
}
