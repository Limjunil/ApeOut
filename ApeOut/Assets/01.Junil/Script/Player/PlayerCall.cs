using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.Create();
    }

    
}
