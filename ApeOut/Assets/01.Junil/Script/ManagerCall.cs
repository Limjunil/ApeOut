using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCall : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.Instance.Create();
        PauseUIControl.Instance.Create();
        TitleMenuControl.Instance.Create();
        SoundManager.Instance.Create();
        OptionUIControl.Instance.Create();
        UIManager.Instance.Create();
        MapManager.Instance.Create();



        GFunc.LoadScene(RDefine.TITLE_SCENE);
    }
}
