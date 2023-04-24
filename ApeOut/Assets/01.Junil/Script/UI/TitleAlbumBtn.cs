using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAlbumBtn : MonoBehaviour
{

    public TMP_Text albumTxt = default;

    public Color normalColor = default;

    public TitleMenuBtnControl menuBtnScrpit = default;

    // Start is called before the first frame update
    void Start()
    {
        albumTxt = gameObject.GetComponent<TMP_Text>();
        normalColor = new Color32(0, 113, 134, 255);

        albumTxt.color = normalColor;

        GameObject menuTxts_ = gameObject.transform.parent.gameObject;

        menuBtnScrpit = menuTxts_.transform.parent.gameObject.GetComponent<TitleMenuBtnControl>();
    }

    public void OnMouseAlbum()
    {
        albumTxt.color = new Color32(210, 210, 210, 255);
    }

    public void OffMouseAlbum()
    {
        albumTxt.color = normalColor;
    }

    public void ClickGameStart()
    {
        GFunc.LoadScene(RDefine.PLAY_SCENE);
    }

    public void CloseAlbum()
    {
        menuBtnScrpit.BackAlbum();
    }
}
