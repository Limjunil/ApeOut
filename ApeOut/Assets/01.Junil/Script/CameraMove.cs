using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public const float SPEED_CAMERA = 1f;

    public float cameraHeight = default;
    public float cameraWidth = default;

    public GameObject targetPlayer = default;

    // 예외 거리 값
    public float exceptionRangeVal = default;

    // 플레이어와 카메라 사이의 거리 값
    public float distFromCamera = default;

    public Vector3 offset = default;

    // Start is called before the first frame update
    void Start()
    {        
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        exceptionRangeVal = 0.4f;

        distFromCamera = 8f;

        offset = new Vector3(0f, 8f, 0f);

        GetTargetPlayer();
    }



    private void LateUpdate()
    {
        gameObject.transform.position = targetPlayer.transform.position + offset;


    }


    public void GetTargetPlayer()
    {

        if (targetPlayer == null || targetPlayer == default)
        {
            targetPlayer = GameObject.Find("Player");
        }
        else
        {
            /* Do Nothing */
        }
    }
}
