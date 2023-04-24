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
    public void SelectedAlbum(string selectmenuName_)
    {
        int temp_ = default;

        isSelectAlbum = true;

        switch (selectmenuName_)
        {
            case "StageOne":
                temp_ = 0;
                break;

            default:

                break;
        }

        titleMenuBtns[temp_].transform.localScale = new Vector3(3f, 3f, 3f);
        titleMenuBtns[temp_].transform.localPosition = Vector3.zero;
        titleMenuBtns[temp_].transform.SetAsLastSibling();

    }

    //! 스테이지에서 다시 앨범 상태로 돌아간다
    public void BackAlbum(string selectmenuName_)
    {

        int temp_ = default;

        isSelectAlbum = false;

        switch (selectmenuName_)
        {
            case "StageOne":
                temp_ = 0;
                break;

            default:

                break;
        }

        titleMenuBtns[temp_].transform.localScale = Vector3.one;

        titleMenuBtns[temp_].transform.SetSiblingIndex(temp_);

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
        if(isSelectAlbum == true) { return; }

        AllOffTitleMenu();

        int temp_ = default;

        switch (selectmenuName_)
        {
            case "StageOne":
                temp_ = 0;
                break;

            case "StageTwo":
                temp_ = 1;

                break;

            case "StageThree":
                temp_ = 2;

                break;

            case "StageFour":
                temp_ = 3;

                break;

            case "BackBtnTxt":
                temp_ = 4;

                break;

            default:
                temp_ = 0;
                return;
        }

        titleMenuBtns[temp_].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);


    }
}
