using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public const float SPEED_CAMERA = 6f;

    public float cameraHeight = default;
    public float cameraWidth = default;

    public GameObject targetPlayer = default;
    
    // 카메라가 현재 잡고 있는 타겟
    public Vector3 nowTarget = default;

    public Vector3 deadOffset = default;
    public Quaternion deadRotate = default;

    public float addPlusX = default;
    public float addPlusZ = default;


    // 예외 거리 값
    public float exceptionRangeVal = default;

    public bool isWallChk = false;


    // { [Junil] 카메라 흔들림 추가
    // 카메라를 흔들어야 하는지 체크하는 bool 값
    public bool isShake = false;

    public float shakeX = default;
    public float shakeZ = default;

    public int shakeForce = default;


    // } [Junil] 카메라 흔들림 추가


    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.mainCamera = this;

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        exceptionRangeVal = 0.4f;

        deadOffset = new Vector3(0f, 10f, 0f);
        deadRotate = Quaternion.Euler(-90f, 0f, 0f);

        isWallChk = false;
        isShake = false;
        nowTarget = default;

        addPlusX = 0f;
        addPlusZ = 0f;

        shakeForce = PlayerPrefs.GetInt("shakeForce");

        gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        Camera.main.backgroundColor = Color.white;

        GetTargetPlayer();
    }



    private void LateUpdate()
    {
        if(PlayerManager.Instance.player.isDead == true)
        {
            return;
        }

        CameraMoveOne();

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



    public void CameraMoveOne()
    {
        if(isShake == true)
        {
            isShake = false;

            StartShakeCam();

        }
                
        var mousPosTest_ = Input.mousePosition;

        mousPosTest_.z = Camera.main.nearClipPlane + 7;

        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(mousPosTest_);

        Vector3 targetPos_ = targetPlayer.transform.position;


        // 마우스 위치에 대한 최대, 최소 값을 구한 값
        float clampMinX = Mathf.Clamp(mousePos_.x,
            targetPos_.x - (cameraWidth * 0.4f), targetPos_.x + (cameraWidth * 0.4f));

        float clampMinZ = Mathf.Clamp(mousePos_.z,
            targetPos_.z - (cameraHeight * 0.4f), targetPos_.z + (cameraHeight * 0.4f));

        
        // 새로운 카메라 위치를 잡아주기 위한 값들
        Vector3 testCamera_ = new Vector3(clampMinX + addPlusX, 10f, clampMinZ + addPlusZ);
        Vector3 targetCameraPos_ = new Vector3(targetPos_.x + addPlusX, 10f, targetPos_.z + addPlusZ);


        nowTarget = testCamera_;

        // 부드러운 움직임을 위해서 Vector3.Lerp 사용
        gameObject.transform.position =
            Vector3.Lerp(transform.position, nowTarget, SPEED_CAMERA * Time.deltaTime);

    }

    //! 플레이어가 죽으면 작동하는 함수
    public void DeadCameraMove()
    {
        StartCoroutine(DeadCamera());
    }

    IEnumerator DeadCamera()
    {
        for(int i = 0; i < 2; i++)
        {
            Vector3 temp_ = gameObject.transform.position;
            temp_.y += 10f;

            gameObject.transform.position = temp_;

            yield return new WaitForSeconds(1f);
        }

        gameObject.transform.position = deadOffset;
        gameObject.transform.rotation = deadRotate;
        Camera.main.backgroundColor = new Color32(180, 40, 40, 255);

        UIManager.Instance.DeadUICall();

    }

    //public void WallCameraMove(Vector3 wallPos_)
    //{
    //    isWallChk = true;

    //    Vector3 testPos_ = wallPos_ - gameObject.transform.position;

    //    Vector3 moveCamera_ = -1f * testPos_;

    //    Vector3 tempWallPos_ = new Vector3(wallPos_.x, 8f, wallPos_.z);
    //    Vector3 targetCameraPos_ = new Vector3(moveCamera_.x, 8f, moveCamera_.z);


    //    addPlusX = moveCamera_.x;
    //    addPlusZ = moveCamera_.z;
    //    //gameObject.transform.position = Vector3.Lerp(transform.position, targetCameraPos_, SPEED_CAMERA * Time.deltaTime);

    //}

    // 카메라의 흔들리는 값은 코루틴으로 구한다
    IEnumerator ShakeCamera = default;


    public void StartShakeCam()
    {
        ShakeCamera = ShakeCameraVal();
        StartCoroutine(ShakeCamera);
    }

    public void StopShakeCam()
    {
        if(ShakeCamera != null)
        {
            StopCoroutine(ShakeCamera);

            addPlusX = 0f;
            addPlusZ = 0f;
        }
    }


    //! 카메라 흔들림을 표현하는 함수
    public IEnumerator ShakeCameraVal()
    {

        float time_ = 0f;

        while (true)
        {
            if(2f <= time_)
            {
                addPlusX = 0f;
                addPlusZ = 0f;

                yield break;
            }

            yield return new WaitForSeconds(0.02f);
            //GFunc.Log("흔들림 호출");

            time_ += 0.1f;

            // Random.insideUnitSphere는 범위 1의 크기 랜덤 값을 가짐

            float randomX_ = Random.insideUnitSphere.x * shakeForce;

            float randomZ_ = Random.insideUnitSphere.z * shakeForce;

            addPlusX = randomX_;
            addPlusZ = randomZ_;


        }

        
    }


}
