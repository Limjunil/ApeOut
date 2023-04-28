using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MapManager : GSingleton<MapManager>
{

    public GameObject[] mapPrefabs = default;

    public List<GameObject> mapPrefabsUse = new List<GameObject>();

    public GameObject mapPosObj = default;

    public LoadingUIControl loadUIControl = default;

    public List<GameObject> waypointPos = new List<GameObject>();

    public int cntMap = 0;

    public override void Start()
    {
        base.Start();


        SceneManager.sceneLoaded += LoadedsceneEvent;

        Scene scene_ = SceneManager.GetActiveScene();

        //if (scene_.name == RDefine.PLAY_SCENE)
        //{
        //    GFunc.Log("맵 호출 1");
        //    SetUpMapManager();
        //}
    }

    public void LoadedsceneEvent(Scene scene_, LoadSceneMode load)
    {
        

        if (scene_.name == RDefine.PLAY_SCENE)
        {
            GFunc.Log("맵 호출 2");
            SetUpMapManager();
        }
    }

    void SetUpMapManager()
    {
        mapPrefabs = new GameObject[4];
        //mapPrefabsUse

        mapPrefabs = Resources.LoadAll<GameObject>("02.YMG/Prefab/Maps/Stage");

        mapPosObj = GFunc.GetRootObj("MapPosObj");


        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        GameObject loadUIObj_ = gameUIObj_.transform.GetChild(3).gameObject;

        loadUIControl = loadUIObj_.GetComponent<LoadingUIControl>();

        cntMap = 0;

        GetMapPrefabs();

        SetStage();
    }

    private void GetMapPrefabs()
    {
        if (mapPrefabsUse.Count != 0)
        {
            mapPrefabsUse.Clear();
        }

        for (int i = 0; i < 4; i++)
        {

            mapPrefabsUse.Add(Instantiate(mapPrefabs[i], Vector3.zero, Quaternion.identity, mapPosObj.transform));

        }


        AllOffStage();

    }

    public override void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    AddCountMap();

        //    SetStage();
        //}
    }

    public void SetStage()
    {
        //loadUIControl.LoadUI();

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
        for (int i = 0; i < 4; i++)
        {
            mapPrefabsUse[i].SetActive(false);
        }
    }

    public void ChangeMap(string mapName_)
    {
        switch (mapName_)
        {
            case "StageOneEnd":
                loadUIControl.LoadUI();

                PlayerManager.Instance.player.transform.localPosition = Vector3.zero;
                AddCountMap();
                SetStage();
                break;

            case "StageTwoEnd":
                loadUIControl.LoadUI();

                PlayerManager.Instance.player.transform.localPosition = Vector3.zero;
                AddCountMap();
                SetStage();
                break;

            case "StageThreeEnd":
                loadUIControl.LoadUI();

                PlayerManager.Instance.player.transform.localPosition = Vector3.zero;
                AddCountMap();
                SetStage();
                break;
        }
    }


}
