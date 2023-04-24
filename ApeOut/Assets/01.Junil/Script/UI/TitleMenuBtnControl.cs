using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleMenuBtnControl : MonoBehaviour
{
    public string menuBtnName = string.Empty;

    public GameObject selectBtn = default;

    public const int ALBUM_CNT = 3;

    public Sprite[] stageOneImgs = new Sprite[ALBUM_CNT];

    public Image stageImgNow = default;

    public GameObject stageMenuObj = default;

    public Vector3 stagePos = default;


    // Start is called before the first frame update
    void Start()
    {
        menuBtnName = gameObject.name;

        selectBtn = gameObject.transform.GetChild(0).gameObject;

        stageImgNow = gameObject.GetComponent<Image>();

        //stagePos = gameObject.transform.position;

        switch (menuBtnName)
        {
            case "StageOne":
                stageOneImgs = Resources.LoadAll<Sprite>("01.Junil/SpriteTwo/Album/Stage1");
                stageImgNow.sprite = stageOneImgs[0];
                stageMenuObj = gameObject.transform.GetChild(1).gameObject;
                stageMenuObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                break;

            default:

                break;
        }


        selectBtn.SetActive(false);
    }

    //! 마우스가 앨범 메뉴 버튼에 올라오면 발동
    public void OnMouseMenuBtn()
    {
        if (TitleMenuControl.Instance.isSelectAlbum == true) { return; }

        TitleMenuControl.Instance.SelectTitleMenu(menuBtnName);
        selectBtn.SetActive(true);

    }



    //! 타이틀로 돌아가는 함수
    public void BackTitle()
    {
        // 타이틀로 돌아가기
        UIManager.Instance.ChangeAlbumToTitle();
    }

    //! 스테이지1에 진입하는 함수
    public void StartStageOne()
    {
        stagePos = gameObject.transform.position;

        // 스테이지 1 로드할 씬 여기에 넣기
        TitleMenuControl.Instance.SelectedAlbum(menuBtnName);
        stageImgNow.sprite = stageOneImgs[1];
        stageMenuObj.transform.localScale = Vector3.one;
        
    }

    public void BackAlbum()
    {
        stageImgNow.sprite = stageOneImgs[0];
        stageMenuObj.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
        gameObject.transform.position = stagePos;

        TitleMenuControl.Instance.BackAlbum(menuBtnName);
    }
}
