using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    public string optionBtnName = string.Empty;
    public GameObject selectBtn = default;

    //FullScreenMode screenMode = default;

    //public bool isFull = false;
    //public int chkScreenVal = 0;

    //public int[] screenWidth = default;
    //public int[] screenHeight = default;

    // Start is called before the first frame update
    void Start()
    {
        
        selectBtn = gameObject.transform.GetChild(0).gameObject;

        optionBtnName = gameObject.name;

        //OptionUIControl.Instance.screenSelet[1].text =
        //    $"{screenWidth[chkScreenVal]} {screenHeight[chkScreenVal]}";

        //OptionUIControl.Instance.fullScreen[1].text = "창모드";

        selectBtn.SetActive(false);
    }

    public void ClickScreen()
    {
        if(OptionUIControl.Instance.chkScreenVal == 4)
        {
            OptionUIControl.Instance.chkScreenVal = 0;
        }
        else
        {
            OptionUIControl.Instance.chkScreenVal++;

        }

        OptionUIControl.Instance.NowOptionText();
    }

    public void ClickFullScreen()
    {
        OptionUIControl.Instance.ChangeFullScreen();
        OptionUIControl.Instance.NowOptionText();
    }

    //! 적용 버튼을 누르면 적용될 버튼
    public void SaveOption()
    {
        OptionUIControl.Instance.SelectScreen();
    }

    public void OnMouseOptionBtn()
    {
        OptionUIControl.Instance.SelectTMPFontUp(optionBtnName);
        selectBtn.SetActive(true);
    }

    public void OffMouseOptionBtn()
    {
        OptionUIControl.Instance.ResetTMPFont();
        selectBtn.SetActive(false);
    }

    //! 취소 버튼을 누르면 다시 타이틀로 돌아가는 함수
    public void BackTitle()
    {
        OptionUIControl.Instance.SelectOpiton();
    }
}
