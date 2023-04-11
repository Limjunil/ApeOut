using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{

    public const float SPEED_CAMERA = 4f;

    public float cameraHeight = default;
    public float cameraWidth = default;

    public GameObject targetPlayer = default;

    // 예외 거리 값
    public float exceptionRangeVal = default;

    // 플레이어와 카메라 사이의 거리 값
    public float distFromCamera = default;



    // Start is called before the first frame update
    void Start()
    {        
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        exceptionRangeVal = 0.4f;

        distFromCamera = 8f;

        GetTargetPlayer();
    }



    private void LateUpdate()
    {



        // 플레이어 위치 값
        Vector3 targetPos_ = new Vector3(targetPlayer.transform.position.x, distFromCamera, targetPlayer.transform.position.z);
        
        // 마우스 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 testMousePos_ = new Vector3(mousePos_.x, distFromCamera, mousePos_.y);



        // 마우스 위치에 대한 최대, 최소 값을 구한 값
        float clampMinX = Mathf.Clamp(mousePos_.x,
            targetPos_.x - (cameraWidth * 0.5f), targetPos_.x + (cameraWidth * 0.5f));

        float clampMinZ = Mathf.Clamp(mousePos_.z,
            targetPos_.z - (cameraHeight * 0.5f), targetPos_.z + (cameraHeight * 0.5f));


        // 새로운 카메라 위치를 잡아주기 위한 값들
        Vector3 testCamera_ = new Vector3(clampMinX, distFromCamera, clampMinZ);
        Vector3 targetCameraPos_ = new Vector3(targetPos_.x, distFromCamera, targetPos_.z);

        // 예외 범위를 지정하여 카메라 위치를 잡는 조건
        if ((targetPos_.x - (cameraWidth * exceptionRangeVal) <= clampMinX && clampMinX <= targetPos_.x + (cameraWidth * exceptionRangeVal)) &&
            (targetPos_.z - (cameraHeight * exceptionRangeVal) <= clampMinZ && clampMinZ <= targetPos_.z + (cameraHeight * exceptionRangeVal)))
        {
            // 부드러운 움직임을 위해서 Vector3.Lerp 사용
            gameObject.transform.position = Vector3.Lerp(transform.position, targetCameraPos_, SPEED_CAMERA * Time.deltaTime);
        }   // if : 예외 범위일 때는 플레이어 위치를 잡는 조건
        else
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, testCamera_, SPEED_CAMERA * Time.deltaTime);

        }   // else : 그 외는 마우스 위치를 잡는 조건
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
