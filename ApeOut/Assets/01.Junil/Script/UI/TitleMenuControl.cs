using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuControl : GSingleton<TitleMenuControl>
{

    public GameObject titleMenuObj = default;

    public const int TITLE_MENU_CNT = 5;

    public GameObject[] titleMenuBtns = new GameObject[TITLE_MENU_CNT];

    public bool isSelectAlbum = false;

    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;


        Scene scene_ = SceneManager.GetActiveScene();

        SetUpTitlemenu(scene_);

    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {
        SetUpTitlemenu(scene_);
    }

    public void SetUpTitlemenu(Scene scene_)
    {

        if (scene_.name != RDefine.TITLE_SCENE)
        {
            return;
        }

        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        titleMenuObj = gameUIObj_.transform.GetChild(1).gameObject;

        GameObject albumBtn_ = titleMenuObj.transform.GetChild(3).gameObject;

        for (int i = 0; i < TITLE_MENU_CNT - 1; i++)
        {
            titleMenuBtns[i] = albumBtn_.transform.GetChild(i).gameObject;
        }

        titleMenuBtns[TITLE_MENU_CNT - 1] = titleMenuObj.transform.GetChild(2).gameObject;

        isSelectAlbum = false;
    }


    //! 앨범이 선택되면 스테이지 정보가 보인다
    public void SelectedAlbum()
    {

    }


    //! 모든 앨범 메뉴들을 전부 1크기로 줄인다
    public void AllOffTitleMenu()
    {
        for(int i = 0; i < TITLE_MENU_CNT; i++)
        {
            titleMenuBtns[i].transform.localScale = Vector3.one;
            titleMenuBtns[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    //! 매개변수에 따라 뭐가 선택되었는지 처리하는 함수
    public void SelectTitleMenu(string selectmenuName_)
    {
        AllOffTitleMenu();

        switch (selectmenuName_)
        {
            case "StageOne":
                titleMenuBtns[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                break;

            case "StageTwo":
                titleMenuBtns[1].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                break;

            case "StageThree":
                titleMenuBtns[2].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                break;

            case "StageFour":
                titleMenuBtns[3].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                break;

            case "BackBtnTxt":
                titleMenuBtns[4].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                break;
        }
    }
}
