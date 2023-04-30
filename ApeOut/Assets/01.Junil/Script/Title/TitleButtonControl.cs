using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonControl : MonoBehaviour
{
    public GameObject selectButton = default;

    private void Awake()
    {
        selectButton = gameObject.transform.GetChild(0).gameObject;
        selectButton.SetActive(false);
    }


    //! 마우스가 버튼에 올라오면 발동
    public void OnMouseButton()
    {
        selectButton.SetActive(true);

    }

    //! 마우스가 버튼을 벗어나면 발동
    public void OffMouseButton()
    {
        selectButton.SetActive(false);

    }


    //! 게임 시작 버튼을 누르면 발동
    public void OnGamePlay()
    {
        UIManager.Instance.ChangeTitleToAlbum();
    }

    //! 옵션 버튼을 누르면 발동
    public void OnOpiton()
    {
        OptionUIControl.Instance.SelectOpiton();

    }

    //! 게임 종료를 누르면 발동
    public void ExitGame()
    {
        GFunc.QuitThisGame();
    }

}
