using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GSingleton<UIManager>
{
    
    public bool isOpenPause = false;

    protected override void Init()
    {
        base.Init();

        isOpenPause = false;
    }


    // Update is called once per frame
    public override void Update()
    {
        // 일시정지 메뉴 열고 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpenPause == false)
            {
                isOpenPause = true;
                PauseUIControl.Instance.OpenPauseMenu();
            }
            else if(isOpenPause == true)
            {
                isOpenPause = false;
                PauseUIControl.Instance.OffPauseMenu();

            }
        }



    }
}
