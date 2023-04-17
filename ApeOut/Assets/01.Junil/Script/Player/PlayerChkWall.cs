using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChkWall : MonoBehaviour
{

    public int collCnt = default;

    public float deleyTime = default;

    public Vector3 chkPos = default;

    // Start is called before the first frame update
    void Start()
    {
        collCnt = 0;
        deleyTime = 0f;
        chkPos = Vector3.zero;
    }

    private void Update()
    {
        //GFunc.Log($"{collCnt}");
        deleyTime += Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if(deleyTime < 1f) { return; }
            collCnt++;
            deleyTime = 0f;

            if(chkPos == other.transform.position || 2 <= collCnt) { return; }
            collCnt++;
            chkPos = other.transform.position;
            PlayerManager.Instance.mainCamera.WallCameraMove(chkPos);
            //gameObject.transform.position = collision.transform.position;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            if(0 < collCnt)
            {
                collCnt--;
            }

            if(collCnt <= 0)
            {
                PlayerManager.Instance.mainCamera.addPlusX = 0f;
                PlayerManager.Instance.mainCamera.addPlusZ = 0f;
                PlayerManager.Instance.mainCamera.isWallChk = false;
            }


            //if (0 < collCnt)
            //{
            //    collCnt--;
            //}

            //if(collCnt <= 0)
            //{
            //    PlayerManager.Instance.mainCamera.addPlusX = 0f;
            //    PlayerManager.Instance.mainCamera.addPlusZ = 0f;

            //    //PlayerManager.Instance.mainCamera.isWallChk = false;
            //}
            //gameObject.transform.position = collision.transform.position;
        }
    }
}
