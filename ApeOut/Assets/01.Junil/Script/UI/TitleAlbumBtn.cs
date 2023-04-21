using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAlbumBtn : MonoBehaviour
{

    public TMP_Text albumTxt = default;

    public Color normalColor = default;

    // Start is called before the first frame update
    void Start()
    {
        albumTxt = gameObject.GetComponent<TMP_Text>();
        normalColor = new Color(189f, 100f, 53f, 100f);

        albumTxt.color = normalColor;
    }

    public void OnMouseAlbum()
    {
        albumTxt.color = Color.white;
    }

    public void OffMouseAlbum()
    {
        albumTxt.color = normalColor;
    }


}
