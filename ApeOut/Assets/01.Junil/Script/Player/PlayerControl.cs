using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerMovement playerMove = default;
    private Animator playerAnimator = default;
    private PlayerInput playerInput = default;
    private PlayerAttack playerAttack = default;


    private void Awake()
    {
        // 플레이어 싱글톤 호출
        PlayerManager.Instance.player = this;

        playerMove = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();


        playerAnimator.SetTrigger("OnIdle");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        LookMousePos();

        PlayerOnMove(playerInput.moveX, playerInput.moveZ);

        if (playerInput.isAttack)
        {
            if (playerAttack.isAttack == false)
            {
                playerAttack.OnAttack();
                playerAnimator.SetTrigger("Attack1");
            }
        }
        //GFunc.Log($"카메라 스크린 월드 : {Camera.main.WorldToScreenPoint(Input.mousePosition)}");
        //GFunc.Log($"플레이어 : {Camera.main.WorldToScreenPoint(gameObject.transform.position)}");

    }

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
    }


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



    }
}
