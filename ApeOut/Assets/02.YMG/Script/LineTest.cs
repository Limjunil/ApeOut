using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    LineRenderer laserLine;
    public Transform laserOrigin;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 2;
    }

    void Update()
    {
        laserLine.SetPosition(0, laserOrigin.position); 
        laserLine.SetPosition(1, transform.forward * 20 + transform.position);

    }
}
