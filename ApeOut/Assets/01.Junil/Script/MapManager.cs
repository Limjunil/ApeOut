using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MapManager : GSingleton<MapManager>
{

    public GameObject[] mapPrefabs = new GameObject[4];

    public GameObject[] mapPrefabsUse = new GameObject[4];

    public GameObject mapPosObj = default;

    public LoadingUIControl loadUIControl = default;

    public int cntMap = 0;

    public override void Start()
    {
        base.Start();

        SceneManager.sceneLoaded += LoadedsceneEvent;

        Scene scene_ = SceneManager.GetActiveScene();

        //if (scene_.name == RDefine.PLAY_SCENE)
        //{
        //    SetUpMapManager();
        //}
    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {
        GFunc.Log("일시정지 호출됨");

        if (scene_.name == RDefine.PLAY_SCENE)
        {
            SetUpMapManager();
        }
    }

    void SetUpMapManager()
    {
        mapPrefabs = Resources.LoadAll<GameObject>("01.Junil/Prefabs/Map");

        mapPosObj = GFunc.GetRootObj("MapPosObj");


        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        GameObject loadUIObj_ = gameUIObj_.transform.GetChild(3).gameObject;

        loadUIControl = loadUIObj_.GetComponent<LoadingUIControl>();

        cntMap = 0;

        GetMapPrefabs();
    }

    private void GetMapPrefabs()
    {
        for(int i = 0; i < 3; i++)
        {
            mapPrefabsUse[i] = Instantiate(mapPrefabs[i], Vector3.zero, Quaternion.identity, mapPosObj.transform);
            
        }

        AllOffStage();

    }

    public void SetStage()
    {
        loadUIControl.LoadUI();

        AllOffStage();

        mapPrefabsUse[cntMap].SetActive(true);

        NavMeshSurface[] surfaces_ = mapPrefabsUse[cntMap].GetComponentsInChildren<NavMeshSurface>();

        foreach (var stage in surfaces_)
        {
            stage.RemoveData();
            stage.BuildNavMesh();
        }
    }

    public void AddCountMap()
    {
        cntMap++;
    }

    public void AllOffStage()
    {
        for (int i = 0; i < 3; i++)
        {
            mapPrefabsUse[i].SetActive(false);
        }
    }
}
