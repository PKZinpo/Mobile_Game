using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapSpawn {

    //public int spawnMin;
    //public int spawnMax;
    public int spawnIndex;
    public int spawnNum;

    //public MapSpawn(int min, int max) {
    //    spawnMin = min;
    //    spawnMax = max;
    //}
}


public class ProceduralGeneration : MonoBehaviour {

    [Header("Map Generation")]
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int spawnDistanceMax;

    
    public delegate void MapSpawnDelegate(Vector3 position);

    //private MapSpawnDelegate MapSpawnFunction;
    private Action<Vector3> MapSpawnAction;

    private float dis;
    private int spawnDistance;
    private GameObject start;

    private int randomSpawnMin = 5;
    private int randomSpawnMax = 10;

    [Header("Coin Spawning")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinRadius;
    [SerializeField] private float upperBound;
    [SerializeField] private float lowerBound;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;

    private MapSpawn coinSpawn;
    
    [Header("Obstacle Spawning")]
    [SerializeField] private GameObject obstaclePrefab;
    [Range(0.1f, 1f)]
    [SerializeField] private float obstacleFrequency;
    
    private int spawnIndex;


    private void Awake() {
        dis = ceiling.GetComponent<Renderer>().bounds.size.x;

        //RandomizeSpawnVariables(coinSpawn);
        //MapSpawnFunction = GenerateMap;
        MapSpawnAction = GenerateObstacle;
        spawnIndex = 0;
    }

    private void Start() {
        start = GameObject.FindGameObjectWithTag("Start");

        for (spawnDistance = 0; spawnDistance < spawnDistanceMax; spawnDistance++) {
            Vector3 targetPos = new Vector3(start.transform.position.x + (spawnDistance * dis), start.transform.position.y, start.transform.position.z);
            if (Physics.Raycast(targetPos, transform.TransformDirection(Vector3.down), Mathf.Infinity)) {
                Debug.Log("[ProceduralGeneration] Map exists");
            }
            else {
                GameObject map = Instantiate(mapPrefab);
                map.transform.position = targetPos;
            }
        }
    }
    private void RandomizeSpawnVariables(MapSpawn spawn) {
        spawn.spawnIndex = UnityEngine.Random.Range(randomSpawnMin, randomSpawnMax);
        spawn.spawnNum = 0;
    }

    public void GenerateMap(Vector3 position) {

        GameObject map = Instantiate(mapPrefab);
        map.transform.position = new Vector3(position.x + (spawnDistanceMax * dis), position.y, position.z);

        MapSpawnAction(map.transform.position);

        //if (coinSpawn. < coinSpawnMap) {
        //    GenerateObstacle(map.transform.position);
        //    coinSpawnNum++;
        //}
        //else {

        //}
        



        //if (Random.Range(0f, 1f) <= obstacleFrequency) {
        //    GenerateObstacle(map.transform.position);
        //}
        //Debug.Log("[ProceduralGeneration] Generating map");
    }

    private void GenerateObstacle(Vector3 position) {
        GameObject obstacle = Instantiate(obstaclePrefab);
        obstacle.transform.position = new Vector3(position.x + UnityEngine.Random.Range(0, dis) - (dis / 2), position.y, position.z);

        //obstacle.GetComponent<GravityManager>().
    }

    private void GenerateCoin(Vector3 position) {

        Vector3 newPos = new Vector3(UnityEngine.Random.Range(position.x + leftBound, position.x + rightBound),
                                     UnityEngine.Random.Range(position.y + lowerBound, position.y + upperBound),
                                     position.z);

        if (Physics.OverlapSphere(newPos, coinRadius).Length > 0) {
            Debug.Log("[ProceduralGeneration] Object in position, finding new position for coin");
            GenerateCoin(position);
        }
        else {
            Debug.Log("[ProceduralGeneration] Position found");
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = newPos;
        }
    }
}
