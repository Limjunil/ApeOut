using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : GSingleton<SoundManager>
{
    // 스테이지 사운드 모음
    public AudioClip[] stageSounds = default;

    public AudioClip[] playerSounds = default;

    public AudioClip mainSounds = default;

    public AudioClip loadingSounds = default;

    public AudioClip enemySounds = default;

    public AudioSource soundSetObj = default;

    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;
        SetUpSoundManager();
    }


    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {
        SetUpSoundManager();
    }

    // Start is called before the first frame update
    void SetUpSoundManager()
    {
        GFunc.Log("사운드 셋업");
        stageSounds = Resources.LoadAll<AudioClip>("01.Junil/Sound/StageSound");
        playerSounds = Resources.LoadAll<AudioClip>("01.Junil/Sound/PlayerSound");
        mainSounds = Resources.Load<AudioClip>("01.Junil/Sound/Main/MenuMusic");
        loadingSounds = Resources.Load<AudioClip>("01.Junil/Sound/Loading/LoadingSound");
        enemySounds = Resources.Load<AudioClip>("01.Junil/Sound/Enemy/DieEnemy");

        GameObject soundObj_ = GFunc.GetRootObj("SoundSetObj");
        soundSetObj = soundObj_.GetComponent<AudioSource>();
        soundSetObj.loop = false;
        soundSetObj.playOnAwake = false;

        SetMainBg();
    }


    public void SetStageBg(string mapName_)
    {
        switch (mapName_)
        {
            case "StageOneEnd":
                soundSetObj.clip = stageSounds[1];
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

            case "StageTwoEnd":
                soundSetObj.clip = stageSounds[2];
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

            case "StageThreeEnd":
                //
                break;

            case "StageFourEnd":
                soundSetObj.clip = mainSounds;
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

            default:
                break;

        }
    }


    public void SetMainBg()
    {
        Scene scene_ = SceneManager.GetActiveScene();

        switch (scene_.name)
        {
            case RDefine.TITLE_SCENE:
                soundSetObj.clip = mainSounds;
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

            case RDefine.PLAY_SCENE:
                soundSetObj.clip = stageSounds[0];
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

            case RDefine.LOADING_SCENE:
                soundSetObj.clip = mainSounds;
                soundSetObj.loop = true;
                soundSetObj.Play();
                break;

        }
    }


}
