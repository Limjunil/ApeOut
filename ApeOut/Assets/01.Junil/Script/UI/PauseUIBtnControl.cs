using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PauseUIBtnControl : MonoBehaviour
{
    public string txtObjName = default;

    // Start is called before the first frame update
    void Start()
    {
        txtObjName = gameObject.name;
        

    }

    //! 마우스가 버튼에 올라오면 발동
    public void OnMouseButton()
    {
        PauseUIControl.Instance.SelectPauseMenu(txtObjName);

    }

    //! 마우스가 재개 버튼을 클릭하면 발동
    public void OnClickResumeBtn()
    {
        GFunc.Log("다시 게임 시작");
        UIManager.Instance.OnOffPauseUI();
    }

    //! 마우스가 재시작 버튼을 클릭하면 발동
    public void OnClickRestartBtn()
    {

        GFunc.Log("게임 재 시작");
        GFunc.LoadScene(RDefine.PLAY_SCENE);
        UIManager.Instance.OnOffPauseUI();
        


    }

    //! 마우스가 메인 메뉴 버튼을 클릭하면 발동
    public void OnClickRetitleBtn()
    {
        
        GFunc.Log("타이틀로 돌아가기");
        UIManager.Instance.Retitle();
    }
}
