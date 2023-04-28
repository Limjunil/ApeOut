using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpewn : GSingleton<EnemySpewn>
{
    public GameObject enemyZoneObj = default;
    public GameObject[] enemyPrefabs = new GameObject[3];

    public GameObject[] enemyNor = new GameObject[7];
    public GameObject[] enemyLaser = new GameObject[7];
    public GameObject[] enemyBoom = new GameObject[6];


    // Start is called before the first frame update
    public override void Start()
    {

        SceneManager.sceneLoaded += LoadedsceneEvent;

        Scene scene_ = SceneManager.GetActiveScene();
    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {

        if (scene_.name == RDefine.PLAY_SCENE)
        {
            enemyPrefabs = Resources.LoadAll<GameObject>("02.YMG/Prefab/Enemys/Use");

            enemyZoneObj = GFunc.GetRootObj("EnemyZone");
        }
    }

    
}
