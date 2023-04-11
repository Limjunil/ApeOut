using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorImgChange : MonoBehaviour
{

    Texture2D cursorImg = default;

    // Start is called before the first frame update
    void Start()
    {
        cursorImg = Resources.Load<Texture2D>("01.Junil/Sprite/CursorImg");

        Vector2 setCursorPos_ = new Vector2(cursorImg.width / 2, cursorImg.height / 2);

        Cursor.SetCursor(cursorImg, setCursorPos_, CursorMode.ForceSoftware);
    }

}
