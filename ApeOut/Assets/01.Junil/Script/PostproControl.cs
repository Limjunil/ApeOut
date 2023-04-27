using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostproControl : MonoBehaviour
{
    public Texture[] cameraFilter = new Texture[6];

    public Volume postProcess = default;

    ColorAdjustments colorAdjustments;

    Bloom bloom;

    ChromaticAberration chromatic;

    private void Start()
    {
        SetPostPro();
    }

    private void FixedUpdate()
    {
        // 화면이 흔들리는 효과
        chromatic.intensity.value = Mathf.PingPong(Time.time * 2, 0.5f);
    }


    public void PauseEffect()
    {
        if(UIManager.Instance.isOpenPause == true)
        {
            colorAdjustments.hueShift.value = -125f;
            colorAdjustments.saturation.value = -100f;
        }
        else
        {
            colorAdjustments.hueShift.value = 0f;
            colorAdjustments.saturation.value = -10f;

        }
        
    }

    public void SetPostPro()
    {
        postProcess = gameObject.GetComponent<Volume>();

        postProcess.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        postProcess.profile.TryGet<Bloom>(out bloom);

        postProcess.profile.TryGet<ChromaticAberration>(out chromatic);


        colorAdjustments.hueShift.value = 0f;
        colorAdjustments.saturation.value = -10f;

        bloom.threshold.value = 0.9f;
    }
}
