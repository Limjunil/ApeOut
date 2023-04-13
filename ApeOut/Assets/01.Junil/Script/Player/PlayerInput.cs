using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // { [Junil] 플레이어의 이동 입력 정보들
    public string moveAxisNameX = "Horizontal";
    public string moveAxisNameZ = "Vertical";


    public float moveX { get; private set; }
    public float moveZ { get; private set; }
    // } [Junil] 플레이어의 이동 입력 정보들

    // { [Junil] 플레이어의 공격과 잡기 버튼 입력 정보들
    public bool isAttack { get; private set; }

    public bool isGrab { get; private set; }
    // } [Junil] 플레이어의 공격과 잡기 버튼 입력 정보들

    //public float cameraMoveX { get; private set; }
    //public float cameraMoveZ { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        moveX = 0f;
        moveZ = 0f;


        isAttack = false;
        isGrab = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw(moveAxisNameX);
        moveZ = Input.GetAxisRaw(moveAxisNameZ);

        isAttack = Input.GetMouseButtonDown(0);
        isGrab = Input.GetMouseButton(1);

        


        if (Input.anyKeyDown)
        {
            
            //GFunc.Log($"{cameraMoveX}, {cameraMoveZ}");

            //GFunc.Log($"{mousePos_}");
        }


    }
}
