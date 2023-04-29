using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : GSingleton<UIManager>
{
    public GameObject playerUIObj = default;

    public SkinnedMeshRenderer playerNowMesh = default;

    public Material[] playerMaterials = new Material[2];

    public bool isOpenPause = false;

    public bool isAlbum = false;

    public string sceneNameNow = string.Empty;

    public GameObject[] titleMenus = new GameObject[2];

    public const int MOVE_POS_VAL = 850;


    public PlayerChkPos playerChkPos = default;


    public GameObject deadUIObj = default;

    public PostproControl postProControl = default;

    public GameObject[] actionTxts = new GameObject[3];

    public GameObject endUIObj = default;



    protected override void Init()
    {
        base.Init();
       
    }

    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;
        Scene scene_ = SceneManager.GetActiveScene();

        OnUIManagerSet(scene_);
    }

    public void LoadedsceneEvent(Scene scene, LoadSceneMode load)
    {
        OnUIManagerSet(scene);
    }


    // Update is called once per frame
    public override void Update()
    {
        if(sceneNameNow == RDefine.PLAY_SCENE)
        {
            if (PlayerManager.Instance.player.isDead == true)
            {
                if (PlayerManager.Instance.mainCamera.isEndDeadScene == true)
                {
                    if (Input.anyKeyDown)
                    {
                        Time.timeScale = 1;
                        LoadingSceneControl.LoadSceneScene(RDefine.PLAY_SCENE);
                    }
                }

                return;
            }
        }

        // 일시정지 메뉴 열고 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 현재 씬이 타이틀
            if (sceneNameNow == RDefine.PLAY_SCENE)
            {
                OnOffPauseUI();

            }
            else
            {
                ChangeAlbumToTitle();

            }

        }


    }


    public void OnActionUI(int countEvent_)
    {
        if (3 <= countEvent_) { return; }

        
        PlayerManager.Instance.mainCamera.isShake = true;

        actionTxts[countEvent_].SetActive(true);

        PlayerManager.Instance.player.eventCount++;

        StartCoroutine(OffActionUI());
    }


    IEnumerator OffActionUI()
    {
        yield return new WaitForSeconds(2f);

        for(int i = 0; i < 3; i++)
        {
            actionTxts[i].SetActive(false);
        }

    }

    public void OnUIManagerSet(Scene scene_)
    {
        isOpenPause = false;

        isAlbum = false;

        sceneNameNow = scene_.name;

        if (sceneNameNow == RDefine.TITLE_SCENE)
        {


            GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

            for (int i = 0; i < 2; i++)
            {
                titleMenus[i] = gameUIObj_.transform.GetChild(i).gameObject;
            }

            
            playerUIObj = GFunc.GetRootObj("Player");
            GameObject playerMesh_ = playerUIObj.transform.GetChild(1).gameObject;

            playerNowMesh = playerMesh_.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();

            playerMaterials = Resources.LoadAll<Material>("01.Junil/Material/UIPlayer");

            playerNowMesh.material = playerMaterials[0];
        }
        else if (scene_.name == RDefine.PLAY_SCENE)
        {
            GameObject playerChkObj_ = GFunc.GetRootObj("PlayerChkPos");

            playerChkPos = playerChkObj_.GetComponent<PlayerChkPos>();

            GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

            deadUIObj = gameUIObj_.transform.GetChild(2).gameObject;
            deadUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

            GameObject postProcess_ = GFunc.GetRootObj("Postprocess");

            postProControl = postProcess_.GetComponent<PostproControl>();

            GameObject actionObj_ = gameUIObj_.transform.GetChild(4).gameObject;
            
            for(int i = 0; i < 3; i++)
            {
                actionTxts[i] = actionObj_.transform.GetChild(i).gameObject;
                actionTxts[i].SetActive(false);
            }

            endUIObj = gameUIObj_.transform.GetChild(5).gameObject;
            endUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        }
    }


    public void DeadUICall()
    {
        deadUIObj.transform.localScale = Vector3.one;
        PauseUIControl.Instance.pauseUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
        PlayerManager.Instance.player.playerHPUIObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        playerChkPos.ViewPlayerWay();
    }


    public void ChangeBearPos(bool isAlbum_)
    {
        if(isAlbum_ == false)
        {
            playerNowMesh.material = playerMaterials[1];
            playerUIObj.transform.localPosition = new Vector3(0.25f, 6f, 0.85f);
            playerUIObj.transform.localRotation = Quaternion.Euler(-70f, -90f, 90f);
        }
        else
        {
            playerNowMesh.material = playerMaterials[0];

            playerUIObj.transform.localPosition = new Vector3(0.25f, 6f, -0.85f);
            playerUIObj.transform.localRotation = Quaternion.Euler(-87f, 90f, 90f);
        }

    }

    //! 타이틀에서 앨범으로 움직이는 함수
    public void ChangeTitleToAlbum()
    {

        StartCoroutine(ChangeTitleMenu(isAlbum));
        ChangeBearPos(isAlbum);

        isAlbum = true;

        
    }


    //! 앨범에서 타이틀로 움직이는 함수
    public void ChangeAlbumToTitle()
    {
        if(isAlbum == true)
        {
            StartCoroutine(ChangeTitleMenu(isAlbum));
            ChangeBearPos(isAlbum);

            isAlbum = false;

        }
    }

    public void Retitle()
    {
        Time.timeScale = 1;

        LoadingSceneControl.LoadSceneScene(RDefine.TITLE_SCENE);
    }


    //! 일시정지 메뉴를 키고 끌 수 있는 함수
    public void OnOffPauseUI()
    {
        if (isOpenPause == false)
        {
            isOpenPause = true;
            Time.timeScale = 0;
        }
        else if (isOpenPause == true)
        {
            isOpenPause = false;
            Time.timeScale = 1;

        }

        postProControl.PauseEffect();
        PauseUIControl.Instance.OpenPauseMenu();

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
