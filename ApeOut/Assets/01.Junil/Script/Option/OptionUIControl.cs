using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class OptionUIControl : GSingleton<OptionUIControl>
{
    // 타이틀 메뉴들
    public GameObject titleTxts = default;
    public GameObject optionTxts = default;


    public const int OPTION_VALUE = 2;

    public TMP_Text[] screenSelet = new TMP_Text[OPTION_VALUE];
    public TMP_Text[] fullScreen = new TMP_Text[OPTION_VALUE];
    public TMP_Text[] soundControl = new TMP_Text[OPTION_VALUE];
    public TMP_Text[] shakeControl = new TMP_Text[OPTION_VALUE];

    public TMP_Text saveBtn = default;
    public TMP_Text cancelBtn = default;

    public const int BEFORE_FONT_SIZE = 50;
    public const int AFTER_FONT_SIZE = 65;

    Vector3 closeUISize = new Vector3(0.0001f, 0.0001f, 0.0001f);

    public bool isOpenOption = false;

    public int[] screenWidth = default;
    public int[] screenHeight = default;

    FullScreenMode screenMode = default;

    public bool isFull = false;
    public int chkScreenVal;

    public int soundVal;
    public int shakeVal;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;

        Scene scene_ = SceneManager.GetActiveScene();

        SetUpOption(scene_);

    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {
        // 초기값을 불러오기
        SetUpOption(scene_);
    }


    //! 옵션 버튼이 눌리면 옵션이 나오는 함수
    public void SelectOpiton()
    {
        if(isOpenOption == false)
        {
            titleTxts.transform.localScale = closeUISize;

            NowOptionText();

            optionTxts.transform.localScale = Vector3.one;

            isOpenOption = true;
        }
        else
        {
            titleTxts.transform.localScale = Vector3.one;
            optionTxts.transform.localScale = closeUISize;

            isOpenOption = false;

        }
    }


    //! 선택되었을 때 그 버튼의 폰트를 키우는 함수
    public void SelectTMPFontUp(string optionName_)
    {
        ResetTMPFont();

        switch (optionName_)
        {
            case "NowScreenTxt":
                screenSelet[0].fontSize = AFTER_FONT_SIZE;
                screenSelet[1].fontSize = AFTER_FONT_SIZE;
                break;

            case "NowScreenStatusTxt":
                fullScreen[0].fontSize = AFTER_FONT_SIZE;
                fullScreen[1].fontSize = AFTER_FONT_SIZE;

                break;

            case "NowSoundTxt":
                soundControl[0].fontSize = AFTER_FONT_SIZE;
                soundControl[1].fontSize = AFTER_FONT_SIZE;

                break;

            case "NowShakeTxt":
                shakeControl[0].fontSize = AFTER_FONT_SIZE;
                shakeControl[1].fontSize = AFTER_FONT_SIZE;

                break;

            case "SaveTxt":
                saveBtn.fontSize = AFTER_FONT_SIZE;

                break;

            case "CancelTxt":
                cancelBtn.fontSize = AFTER_FONT_SIZE;

                break;
        }
    }

    //! 모든 폰트의 크기를 원래대로 하는 함수
    public void ResetTMPFont()
    {
        for (int i = 0; i < OPTION_VALUE; i++)
        {
            screenSelet[i].fontSize = BEFORE_FONT_SIZE;
            fullScreen[i].fontSize = BEFORE_FONT_SIZE;
            soundControl[i].fontSize = BEFORE_FONT_SIZE;
            shakeControl[i].fontSize = BEFORE_FONT_SIZE;
            
        }

        saveBtn.fontSize = BEFORE_FONT_SIZE;
        cancelBtn.fontSize = BEFORE_FONT_SIZE;
    }

    public void NowOptionText()
    {
        screenSelet[1].text = $"{screenWidth[chkScreenVal]} {screenHeight[chkScreenVal]}";


        if (isFull == false)
        {
            fullScreen[1].text = "창모드";
        }
        else
        {
            fullScreen[1].text = "전체 화면";

        }

        soundControl[1].text = $"{soundVal}";

        shakeControl[1].text = $"{shakeVal}";

    }


    public void ChangeFullScreen()
    {
        if (isFull == false)
        {
            isFull = true;

        }
        else
        {
            isFull = false;
        }

        FullScreenBtn(isFull);

    }

    public void FullScreenBtn(bool isFull)
    {
        if (isFull == false)
        {
            screenMode = FullScreenMode.Windowed;
        }
        else
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }
    }


    public void SelectScreen()
    {
        Screen.SetResolution(
            screenWidth[chkScreenVal], screenHeight[chkScreenVal], screenMode);
    }


    //! 옵션 UI 가져오기
    public void SetUpOption(Scene scene_)
    {
        if (scene_.name != RDefine.TITLE_SCENE)
        {
            
            return;
        }

        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        GameObject titleMenuObj_ = gameUIObj_.transform.GetChild(0).gameObject;

        titleTxts = titleMenuObj_.transform.GetChild(1).gameObject;
        optionTxts = titleMenuObj_.transform.GetChild(2).gameObject;

        GameObject screenSeletObjs_ = optionTxts.transform.GetChild(0).gameObject;
        GameObject fullScreenObjs_ = optionTxts.transform.GetChild(1).gameObject;
        GameObject soundControlObjs_ = optionTxts.transform.GetChild(2).gameObject;
        GameObject shakeControlObjs_ = optionTxts.transform.GetChild(3).gameObject;


        for (int i = 0; i < OPTION_VALUE; i++)
        {
            screenSelet[i] = screenSeletObjs_.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();
            fullScreen[i] = fullScreenObjs_.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();
            soundControl[i] = soundControlObjs_.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();
            shakeControl[i] = shakeControlObjs_.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();

        }

        saveBtn = optionTxts.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        cancelBtn = optionTxts.transform.GetChild(5).gameObject.GetComponent<TMP_Text>();


        isOpenOption = false;


        isFull = false;

        if (chkScreenVal == default)
        {
            chkScreenVal = 0;
        }

        screenWidth = new int[] { 1280, 1360, 1600, 1760, 1920 };

        screenHeight = new int[] { 720, 765, 900, 990, 1080 };

        if (soundVal == default)
        {
            soundVal = 10;
        }

        if (shakeVal == default)
        {
            shakeVal = 5;
        }


        optionTxts.transform.localScale = closeUISize;
    }



}
