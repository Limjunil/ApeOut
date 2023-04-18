using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuBtnControl : MonoBehaviour
{
    public string menuBtnName = string.Empty;

    public GameObject selectBtn = default;

    // Start is called before the first frame update
    void Start()
    {
        menuBtnName = gameObject.name;

        selectBtn = gameObject.transform.GetChild(0).gameObject;

        selectBtn.SetActive(false);
    }

    //! 마우스가 앨범 메뉴 버튼에 올라오면 발동
    public void OnMouseMenuBtn()
    {
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
        // 스테이지 1 로드할 씬 여기에 넣기
        GFunc.LoadScene(RDefine.PLAY_SCENE);
    }
}
