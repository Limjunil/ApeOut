using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBtn : MonoBehaviour
{
    public string optionBtnName = string.Empty;
    public GameObject selectBtn = default;

    // Start is called before the first frame update
    void Start()
    {
        selectBtn = gameObject.transform.GetChild(0).gameObject;

        optionBtnName = gameObject.name;

        selectBtn.SetActive(false);
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
