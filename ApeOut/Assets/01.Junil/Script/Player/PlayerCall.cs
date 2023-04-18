using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.Create();
        // pauseUI 싱글톤 호출
        PauseUIControl.Instance.Create();
        TitleMenuControl.Instance.Create();
        UIManager.Instance.Create();
    }

    
}
