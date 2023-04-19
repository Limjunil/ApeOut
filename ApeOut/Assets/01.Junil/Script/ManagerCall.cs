using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCall : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.Instance.Create();
        // pauseUI 싱글톤 호출
        PauseUIControl.Instance.Create();
        TitleMenuControl.Instance.Create();
        OptionUIControl.Instance.Create();
        UIManager.Instance.Create();


        GFunc.LoadScene(RDefine.TITLE_SCENE);
    }
}
