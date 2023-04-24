using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


public class OptionUIControl : GSingleton<OptionUIControl>
{
    // 타이틀 메뉴들
    public GameObject titleTxt = default;
    public GameObject titleBtns = default;
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

    public int[] screenWidth;
    public int[] screenHeight;

    FullScreenMode screenMode;

    public bool isFull = false;
    public int chkScreenVal;

    public int soundVal;
    public int shakeVal;


    public bool isFullTemp;
    public int chkScreenValTemp;
    public int shakeValTemp;

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


    public void SetNowOption()
    {
        isFullTemp = isFull;
        chkScreenValTemp = chkScreenVal;
        shakeValTemp = shakeVal;

        if (isFull == false)
        {
            PlayerPrefs.SetInt("fullChk", System.Convert.ToInt32(0));
        }
        else
        {
            PlayerPrefs.SetInt("fullChk", System.Convert.ToInt32(1));
        }

        PlayerPrefs.SetInt("chkScreenVal", chkScreenVal);

        PlayerPrefs.SetInt("shakeForce", shakeVal);
    }


    //! 옵션 버튼이 눌리면 옵션이 나오는 함수
    public void SelectOpiton()
    {
        if(isOpenOption == false)
        {
            StartCoroutine(MoveTitle(isOpenOption));
            titleBtns.transform.localScale = closeUISize;

            // 

            SetNowOption();
            NowOptionText();

            optionTxts.transform.localScale = Vector3.one;

            isOpenOption = true;
        }
        else
        {
            StartCoroutine(MoveTitle(isOpenOption));

            isFull = isFullTemp;
            chkScreenVal = chkScreenValTemp;
            shakeVal = shakeValTemp;

            titleBtns.transform.localScale = Vector3.one;
            optionTxts.transform.localScale = closeUISize;

            isOpenOption = false;

        }
    }

    //! 옵션 버튼을 입력시 타이틀이 올라가거나 내려오는 함수
    IEnumerator MoveTitle(bool isOpenOption_)
    {
        float time_ = 0f;
        float posTitleTxt_ = 0f;

        while (true)
        {
            if (1f <= time_)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.1f);

            time_ += 0.2f;

            if (isOpenOption_ == false)
            {
                posTitleTxt_ = UIManager.Instance.EaseInOutBack(150f, 450f, time_);
            }
            else
            {
                posTitleTxt_ = UIManager.Instance.EaseInOutBack(450f, 150f, time_);
            }

            titleTxt.transform.localPosition = new Vector3(0f, posTitleTxt_, 0f);

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
        SetNowOption();

        FullScreenBtn(isFull);

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

        titleTxt = titleMenuObj_.transform.GetChild(1).gameObject;
        titleBtns = titleMenuObj_.transform.GetChild(2).gameObject;
        optionTxts = titleMenuObj_.transform.GetChild(3).gameObject;

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

        //Full = false;

        isFull = System.Convert.ToBoolean(PlayerPrefs.GetInt("fullChk"));

        chkScreenVal = PlayerPrefs.GetInt("chkScreenVal");

        screenWidth = new int[] { 1280, 1366, 1600, 1760, 1920 };

        screenHeight = new int[] { 720, 768, 900, 990, 1080 };

        if (soundVal == default)
        {
            soundVal = 10;
        }

        shakeVal = PlayerPrefs.GetInt("shakeForce");

        if(shakeVal == 0)
        {
            PlayerPrefs.SetInt("shakeForce", 5);
            shakeVal = PlayerPrefs.GetInt("shakeForce");

        }


        optionTxts.transform.localScale = closeUISize;

        NowOptionText();
        SelectScreen();
    }



}
