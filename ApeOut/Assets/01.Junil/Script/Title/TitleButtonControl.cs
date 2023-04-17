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


    // 마우스가 버튼에 올라오면 발동
    public void OnMouseButton()
    {
        selectButton.SetActive(true);

    }

    // 마우스가 버튼을 벗어나면 발동
    public void OffMouseButton()
    {
        selectButton.SetActive(false);

    }
}
