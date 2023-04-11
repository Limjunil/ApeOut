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
            playerAttack.OnAttack();
        }


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
        //Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hitResult_;

        //if(Physics.Raycast(ray_, out hitResult_))
        //{
        //    Vector3 len_ = hitResult_.transform.position - transform.position;

        //    //Vector3 mouseDir_ = new Vector3(hitResult_.point.x,
        //    //    transform.position.y,
        //    //    hitResult_.point.z) - transform.position;

        //    float lookY_ = Mathf.Atan2(len_.x, len_.z);

        //    float rotateY_ = lookY_ * Mathf.Rad2Deg;

        //    transform.rotation = Quaternion.Euler(0, rotateY_, 0);
        //}


        //Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        //Vector3 len_ = mousePos_ - transform.position;

        //float lookY_ = Mathf.Atan2(len_.x, len_.z);

        //float rotateY_ = lookY_ * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, rotateY_, 0);


        //playerAnimator.SetFloat("inputX", len_.x);
        //playerAnimator.SetFloat("inputZ", len_.z);

    }
}
