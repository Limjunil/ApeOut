using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChkPos : MonoBehaviour
{

    public LineRenderer playerChkPos = default;

    int setPosCnt = 0;


    private void OnDisable()
    {
        StopPlayerChk();

    }


    // Start is called before the first frame update
    void Start()
    {
        playerChkPos = gameObject.GetComponent<LineRenderer>();

        StartPlayerChk();

    }

    
    public void ViewPlayerWay()
    {

        StopPlayerChk();

        playerChkPos.positionCount = setPosCnt + 1;

        Vector3 playerPos_ = PlayerManager.Instance.player.transform.position;
        playerPos_.y = 0f;

        playerChkPos.SetPosition(setPosCnt, playerPos_);

        playerChkPos.enabled = true;
    }

    IEnumerator SetPlayerChk = default;

    public void StartPlayerChk()
    {
        StopPlayerChk();

        playerChkPos.positionCount = 0;

        setPosCnt = 0;

        playerChkPos.enabled = false;

        SetPlayerChk = StartChkPlayerPos();

        StartCoroutine(SetPlayerChk);
    }

    public void StopPlayerChk()
    {
        if(SetPlayerChk != null)
        {
            StopCoroutine(SetPlayerChk);
        }
    }


    public IEnumerator StartChkPlayerPos()
    {
        while (true)
        {
            playerChkPos.positionCount = setPosCnt + 1;

            Vector3 playerPos_ = PlayerManager.Instance.player.transform.position;
            playerPos_.y = 0f;

            playerChkPos.SetPosition(setPosCnt, playerPos_);

            setPosCnt++;

            yield return new WaitForSeconds(1f);
        }
    }
}
