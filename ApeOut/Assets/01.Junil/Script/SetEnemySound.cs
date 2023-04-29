using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemySound : MonoBehaviour
{
    public AudioSource enemySoundObj = default;

    // Start is called before the first frame update
    void Start()
    {
        enemySoundObj = gameObject.GetComponent<AudioSource>();
        enemySoundObj.volume = OptionUIControl.Instance.soundVal / (float)10;
    }

}
