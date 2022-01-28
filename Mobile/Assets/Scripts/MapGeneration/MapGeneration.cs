using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public delegate void MapSpawnDelegate(Vector3 position, float xDis, float yDis);

    [Header("Map Generation")]
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int spawnDistanceMax;

    private MapSpawnDelegate MapSpawnFunction;
    private Queue<MapSpawnDelegate> spawnQueue = new Queue<MapSpawnDelegate>();

    private float xDis;
    private float yDis;
    private int spawnDistance;
    private GameObject start;

    private ObjectGeneration mainObstacleGeneration;
    private Transform mapParentTransform;

    private void Awake() {
        xDis = ceiling.GetComponent<Renderer>().bounds.size.x;
        yDis = 20f - ceiling.GetComponent<Renderer>().bounds.size.y;

        mainObstacleGeneration = transform.GetChild(0).GetComponent<ObjectGeneration>();
        mapParentTransform = GameObject.FindGameObjectWithTag("MapParent").transform;
    }

    private void Start() {
        start = GameObject.FindGameObjectWithTag("Start");

        for (spawnDistance = 0; spawnDistance < spawnDistanceMax; spawnDistance++) {
            Vector3 targetPos = new Vector3(start.transform.position.x + (spawnDistance * xDis), start.transform.position.y, start.transform.position.z);
            if (Physics.Raycast(targetPos, transform.TransformDirection(Vector3.down), Mathf.Infinity)) {
                Debug.Log("[MapGeneration] Map exists");
            }
            else {
                GameObject map = Instantiate(mapPrefab, mapParentTransform);
                map.transform.position = targetPos;
            }
        }
    }

    public void GenerateMap(Vector3 position) {

        GameObject map = Instantiate(mapPrefab, mapParentTransform);
        map.transform.position = new Vector3(position.x + (spawnDistanceMax * xDis), position.y, position.z);

        if (spawnQueue.Count > 0) {
            MapSpawnFunction = spawnQueue.Dequeue();
            MapSpawnFunction(map.transform.position, xDis, yDis);
        }
        mainObstacleGeneration.ObjectPrepareForQueue();
    }
    public void AddToSpawnQueue(MapSpawnDelegate function) {
        spawnQueue.Enqueue(function);
    }
}
