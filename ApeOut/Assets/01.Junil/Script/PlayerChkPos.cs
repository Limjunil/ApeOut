using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChkPos : MonoBehaviour
{

    public LineRenderer playerChkPos = default;

    int setPosCnt = 0;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerChkPos = gameObject.GetComponent<LineRenderer>();

        setPosCnt = 0;

        isDead = false;

        playerChkPos.enabled = false;

        StartPlayerChk();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerChkPos.enabled = true;
            isDead = true;
            StopPlayerChk();

        }
    }

    IEnumerator SetPlayerChk = default;

    public void StartPlayerChk()
    {
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
        while (isDead == false)
        {
            playerChkPos.positionCount = setPosCnt + 1;

            Vector3 playerPos_ = PlayerManager.Instance.player.transform.position;

            //playerPos_.y += 1f;

            playerChkPos.SetPosition(setPosCnt, playerPos_);

            setPosCnt++;

            yield return new WaitForSeconds(1f);
        }
    }
}
