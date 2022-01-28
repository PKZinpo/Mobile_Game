using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [Header("Map Generation")]
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int spawnDistanceMax;

    
    public delegate void MapSpawnDelegate(Vector3 position, float xDis, float yDis);

    private MapSpawnDelegate MapSpawnFunction;
    private Queue<MapSpawnDelegate> spawnQueue = new Queue<MapSpawnDelegate>();

    private float xDis;
    private float yDis;
    private int spawnDistance;
    private GameObject start;

    private ObjectGeneration mainObstacleGeneration;

    [Header("Coin Spawning")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinRadius;
    [SerializeField] private float upperBound;
    [SerializeField] private float lowerBound;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;

    [Header("Obstacle Spawning")]
    [SerializeField] private GameObject obstaclePrefab;
    [Range(0.1f, 1f)]
    [SerializeField] private float obstacleFrequency;


    private void Awake() {
        xDis = ceiling.GetComponent<Renderer>().bounds.size.x;
        yDis = 20f - ceiling.GetComponent<Renderer>().bounds.size.y;

        mainObstacleGeneration = transform.GetChild(0).GetComponent<ObjectGeneration>();
    }

    private void Start() {
        start = GameObject.FindGameObjectWithTag("Start");

        for (spawnDistance = 0; spawnDistance < spawnDistanceMax; spawnDistance++) {
            Vector3 targetPos = new Vector3(start.transform.position.x + (spawnDistance * xDis), start.transform.position.y, start.transform.position.z);
            if (Physics.Raycast(targetPos, transform.TransformDirection(Vector3.down), Mathf.Infinity)) {
                Debug.Log("[MapGeneration] Map exists");
            }
            else {
                GameObject map = Instantiate(mapPrefab);
                map.transform.position = targetPos;
            }
        }
    }

    public void GenerateMap(Vector3 position) {

        GameObject map = Instantiate(mapPrefab);
        map.transform.position = new Vector3(position.x + (spawnDistanceMax * xDis), position.y, position.z);

        if (spawnQueue.Count > 0) {
            MapSpawnFunction = spawnQueue.Dequeue();
            MapSpawnFunction(map.transform.position, xDis, yDis);
        }
        mainObstacleGeneration.ObjectPrepareForQueue();
    }

    private void GenerateObstacle(Vector3 position) {
        GameObject obstacle = Instantiate(obstaclePrefab);
        obstacle.transform.position = new Vector3(position.x + UnityEngine.Random.Range(0, xDis) - (xDis / 2), position.y, position.z);

    }

    private void GenerateCoin(Vector3 position) {

        Vector3 newPos = new Vector3(UnityEngine.Random.Range(position.x + leftBound, position.x + rightBound),
                                     UnityEngine.Random.Range(position.y + lowerBound, position.y + upperBound),
                                     position.z);

        if (Physics.OverlapSphere(newPos, coinRadius).Length > 0) {
            Debug.Log("[MapGeneration] Object in position, finding new position for coin");
            GenerateCoin(position);
        }
        else {
            Debug.Log("[MapGeneration] Position found");
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = newPos;
        }
    }
    public void AddToSpawnQueue(MapSpawnDelegate function) {
        spawnQueue.Enqueue(function);
    }
}
