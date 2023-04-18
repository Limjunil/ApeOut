using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : GSingleton<UIManager>
{
    
    public bool isOpenPause = false;

    public bool isAlbum = false;

    public string sceneNameNow = string.Empty;

    public GameObject[] titleMenus = new GameObject[2];

    public const int MOVE_POS_VAL = 850;

    protected override void Init()
    {
        base.Init();

        isOpenPause = false;

        isAlbum = false;
               

                
    }

    public override void Awake()
    {
        base.Awake();

        sceneNameNow = SceneManager.GetActiveScene().name;

        if (sceneNameNow == "JunilTestTitleScene")
        {
            GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

            for (int i = 0; i < 2; i++)
            {
                titleMenus[i] = gameUIObj_.transform.GetChild(i).gameObject;
            }
        }

        
    }


    // Update is called once per frame
    public override void Update()
    {
        
        // 일시정지 메뉴 열고 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 현재 씬이 타이틀
            if (sceneNameNow == "JunilTestTitleScene")
            {
                ChangeAlbumToTitle();
            }
            else
            {
                OnOffPauseUI();
            }

        }


    }


    //! 타이틀에서 앨범으로 움직이는 함수
    public void ChangeTitleToAlbum()
    {

        StartCoroutine(ChangeTitleMenu(isAlbum));

        isAlbum = true;

        
    }


    //! 앨범에서 타이틀로 움직이는 함수
    public void ChangeAlbumToTitle()
    {
        if(isAlbum == true)
        {
            StartCoroutine(ChangeTitleMenu(isAlbum));

            isAlbum = false;

        }
    }


    //! 일시정지 메뉴를 키고 끌 수 있는 함수
    public void OnOffPauseUI()
    {
        if (isOpenPause == false)
        {
            isOpenPause = true;
            PauseUIControl.Instance.OpenPauseMenu();
        }
        else if (isOpenPause == true)
        {
            isOpenPause = false;
            PauseUIControl.Instance.OffPauseMenu();

        }
    }

    IEnumerator ChangeTitleMenu(bool isAlbum)
    {
        float time_ = 0f;
        float posTitle_ = 0f;
        float posAlbum_ = 0f;


        while (true)
        {
            if(1f<= time_)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.1f);

            time_ += 0.2f;

            if(isAlbum == false)
            {
                posTitle_ = EaseInOutBack(0f, MOVE_POS_VAL, time_);
                posAlbum_ = EaseInOutBack(-1 * MOVE_POS_VAL, 0f, time_);
            }
            else
            {
                posTitle_ = EaseInOutBack(MOVE_POS_VAL, 0f, time_);
                posAlbum_ = EaseInOutBack(0f, -1 * MOVE_POS_VAL, time_);
            }

            titleMenus[0].transform.localPosition = new Vector3(0f, posTitle_, 0f);
            titleMenus[1].transform.localPosition = new Vector3(0f, posAlbum_, 0f);

        }

    }
    public float EaseInOutBack(float start, float end, float value)
    {

        float s = 1.70158f;

        end -= start;

        value = (value / 1) - 1;

        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }


}
