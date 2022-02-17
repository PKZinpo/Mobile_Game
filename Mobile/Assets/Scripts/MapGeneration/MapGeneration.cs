using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public delegate void MapSpawnDelegate(Vector3 position, float xDis, float yDis);

    [Header("Map Generation")]
    [SerializeField] private GameObject[] mapPrefab;
    [SerializeField] private int spawnDistanceMax;
    [SerializeField] private int mapChangeScore;

    private MapSpawnDelegate MapSpawnFunction;
    private Queue<MapSpawnDelegate> spawnQueue = new Queue<MapSpawnDelegate>();

    private float xDis;
    private float yDis;
    //private int spawnDistance;
    private GameObject start;
    private GameObject mapToSpawn;
    private Transform mapParentTransform;

    private ObjectGeneration mainObstacleGeneration;
    private PlayerStatsManager psm;

    private int mapPrefabIndex;
    private int prevScore;

    private Vector3 prevMapSpawnPos;
    private float prevMapXHalfDis;
    
    private void Awake() {
        prevScore = 0;
        mapPrefabIndex = 0;
        mapToSpawn = mapPrefab[mapPrefabIndex];

        SetDistances();

        mainObstacleGeneration = transform.GetChild(0).GetComponent<ObjectGeneration>();
        mapParentTransform = GameObject.FindGameObjectWithTag("MapParent").transform;

        psm = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>();
    }

    private void Start() {
        start = GameObject.FindGameObjectWithTag("Start");

        for (int i = 0; i < spawnDistanceMax; i++) {
            Vector3 targetPos = new Vector3(start.transform.position.x + (i * xDis), start.transform.position.y, start.transform.position.z);
            if (Physics.Raycast(targetPos, transform.TransformDirection(Vector3.down), Mathf.Infinity)) {
                Debug.Log("[MapGeneration] Map exists");
            }
            else {
                GameObject map = Instantiate(mapToSpawn, mapParentTransform);
                map.transform.position = targetPos;
            }
            prevMapSpawnPos = targetPos;
            prevMapXHalfDis = mapToSpawn.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x / 2f;
        }
    }

    public void GenerateMap(Vector3 position) {
        float newMapXHalfDis = mapToSpawn.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x / 2f;
        Vector3 newMapSpawnPos = new Vector3 (prevMapSpawnPos.x + newMapXHalfDis + prevMapXHalfDis, prevMapSpawnPos.y, prevMapSpawnPos.z);

        GameObject map = Instantiate(mapToSpawn, mapParentTransform);
        //map.transform.position = new Vector3(position.x + (spawnDistanceMax * xDis), position.y, position.z);
        map.transform.position = newMapSpawnPos;
        prevMapSpawnPos = newMapSpawnPos;
        prevMapXHalfDis = newMapXHalfDis;

        if (spawnQueue.Count > 0) {
            MapSpawnFunction = spawnQueue.Dequeue();
            MapSpawnFunction(map.transform.position, xDis, yDis);
        }
        mainObstacleGeneration.ObjectPrepareForQueue();
    }
    public void AddToSpawnQueue(MapSpawnDelegate function) {
        spawnQueue.Enqueue(function);
    }
    public void ChangeMapPrefab() {
        if (psm.PlayerScore - prevScore <= mapChangeScore * (mapPrefabIndex + 1)) return;
        mapPrefabIndex++;
        mapPrefabIndex = Mathf.Min(mapPrefabIndex, mapPrefab.Length - 1);
        mapToSpawn = mapPrefab[mapPrefabIndex];
        SetDistances();
        Debug.Log("[MapGeneration] Difficulty increased to " + mapPrefabIndex);
    }
    private void SetDistances() {
        xDis = mapToSpawn.transform.GetChild(mapPrefabIndex).GetComponent<Renderer>().bounds.size.x;
        yDis = 20f - mapToSpawn.transform.GetChild(mapPrefabIndex).GetComponent<Renderer>().bounds.size.y;
    }
}
