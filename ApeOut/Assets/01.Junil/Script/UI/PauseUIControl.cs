using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIControl : GSingleton<PauseUIControl>
{

    public GameObject pauseUIObj = default;

    public const int PAUSE_MENU_CNT = 3;

    public TMP_Text[] gameUITxt = new TMP_Text[PAUSE_MENU_CNT];


    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    
    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;

        Scene scene_ = SceneManager.GetActiveScene();

        if (scene_.name == RDefine.PLAY_SCENE)
        {
            SetUpPauseUI();
        }
    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {

        if (scene_.name == RDefine.PLAY_SCENE)
        {
            SetUpPauseUI();
        }
    }


    public void SetUpPauseUI()
    {
        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        pauseUIObj = gameUIObj_.transform.GetChild(0).gameObject;

        GameObject pauseUITxt_ = pauseUIObj.transform.GetChild(1).gameObject;


        for (int i = 0; i < PAUSE_MENU_CNT; i++)
        {
            gameUITxt[i] = pauseUITxt_.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();
        }

        // UI 창을 끄지 않고 줄여놓는다.
        pauseUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

    }

    //! 일시정지 메뉴를 여는 함수
    public void OpenPauseMenu()
    {
        if(UIManager.Instance.isOpenPause == true)
        {
            FirstOpenPauseMenu();

            pauseUIObj.transform.localScale = Vector3.one;
        }
        else
        {
            pauseUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        }

    }


    //! 일시정지 메뉴의 초기 값 설정
    public void FirstOpenPauseMenu()
    {
        SelectPauseMenu("ResumeTxt");
    }

    //! 일시정지 메뉴를 전부 끄는 함수
    public void OffAllMenu()
    {
        for(int i = 0; i < PAUSE_MENU_CNT; i++)
        {
            gameUITxt[i].color = Color.gray;
        }
    }
    
    //! 매개변수에 따라 뭐가 선택되었는지 처리하는 함수
    public void SelectPauseMenu(string selectTxtName_)
    {
        OffAllMenu();

        switch (selectTxtName_)
        {
            case "ResumeTxt":
                gameUITxt[0].color = Color.white;

                break;

            case "RestartTxt":
                gameUITxt[1].color = Color.white;

                break;

            case "RetitleTxt":
                gameUITxt[2].color = Color.white;

                break;
        }
    }

}
