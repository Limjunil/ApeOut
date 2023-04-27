using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{

    public GameObject[] mapPrefabs = new GameObject[3];

    public GameObject[] mapPrefabsUse = new GameObject[3];

    public GameObject mapPosObj = default;

    public LoadingUIControl loadUIControl = default;

    // Start is called before the first frame update
    void Start()
    {
        mapPrefabs = Resources.LoadAll<GameObject>("01.Junil/Prefabs/Map");

        mapPosObj = GFunc.GetRootObj("MapPosObj");


        GameObject gameUIObj_ = GFunc.GetRootObj("GameUIView");

        GameObject loadUIObj_ = gameUIObj_.transform.GetChild(3).gameObject;

        loadUIControl = loadUIObj_.GetComponent<LoadingUIControl>();

        GetMapPrefabs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetStageOne();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SetStageTwo();
        }
    }


    private void GetMapPrefabs()
    {
        for(int i = 0; i < 3; i++)
        {
            mapPrefabsUse[i] = Instantiate(mapPrefabs[i], Vector3.zero, Quaternion.identity, mapPosObj.transform);
            
        }

        AllOffStage();

    }

    public void SetStageOne()
    {
        AllOffStage();

        mapPrefabsUse[0].SetActive(true);

        NavMeshSurface[] surfaces_ = mapPrefabsUse[0].GetComponentsInChildren<NavMeshSurface>();

        foreach (var stage in surfaces_)
        {
            stage.RemoveData();
            stage.BuildNavMesh();
        }
    }

    public void SetStageTwo()
    {
        AllOffStage();

        mapPrefabsUse[1].SetActive(true);

        NavMeshSurface[] surfaces_ = mapPrefabsUse[1].GetComponentsInChildren<NavMeshSurface>();

        foreach (var stage in surfaces_)
        {
            stage.RemoveData();
            stage.BuildNavMesh();
        }
    }

    public void SetStageThree()
    {
        AllOffStage();

        mapPrefabsUse[2].SetActive(true);

        NavMeshSurface[] surfaces_ = mapPrefabsUse[2].GetComponentsInChildren<NavMeshSurface>();

        foreach (var stage in surfaces_)
        {
            stage.RemoveData();
            stage.BuildNavMesh();
        }
    }


    public void AllOffStage()
    {
        for (int i = 0; i < 3; i++)
        {
            mapPrefabsUse[i].SetActive(false);
        }
    }
}
