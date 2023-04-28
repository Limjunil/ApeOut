using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    public string StageChkName = string.Empty;

    private void Start()
    {
        StageChkName = gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == RDefine.PLAYER_TAG)
        {
            MapManager.Instance.ChangeMap(StageChkName);
        }
    }
}
