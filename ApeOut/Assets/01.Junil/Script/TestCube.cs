using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousPosTest_ = Input.mousePosition;
        mousPosTest_.z = Camera.main.nearClipPlane + 7;


        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(mousPosTest_);

        transform.position = mousePos_;
    }
}
