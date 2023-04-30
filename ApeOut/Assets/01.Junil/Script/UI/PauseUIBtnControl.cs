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
        UIManager.Instance.OnOffPauseUI();
    }

    //! 마우스가 재시작 버튼을 클릭하면 발동
    public void OnClickRestartBtn()
    {

        Time.timeScale = 1;
        LoadingSceneControl.LoadSceneScene(RDefine.PLAY_SCENE);



    }

    //! 마우스가 메인 메뉴 버튼을 클릭하면 발동
    public void OnClickRetitleBtn()
    {
        
        UIManager.Instance.Retitle();
    }

    
}
