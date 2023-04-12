using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{

    public CameraMove cameraMove = default;

    // Start is called before the first frame update
    void Start()
    {
        cameraMove = gameObject.transform.parent.gameObject.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            GFunc.Log("실행됨");
            //cameraMove.isWall = true;
            //WallMousePos(other.gameObject);

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            //cameraMove.isWall = false;

        }
    }


    public void WallMousePos(GameObject stopPos_)
    {
        //var mousPosTest_ = stopPos_.transform.position;

        //Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(mousPosTest_);

        //Vector3 playerCameraPos_ = new Vector3(mousePos_.x, 8f, mousePos_.z);

        //cameraMove.transform.position = playerCameraPos_;
    }
}
