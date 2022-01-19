using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour {

    [Header("Map Generation")]
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int spawnDistanceMax;

    private float dis;
    private int spawnDistance;
    private GameObject start;

    [Header("Coin Spawning")]
    [SerializeField] private GameObject coinPrefab;
    [Range(0.1f, 1f)]
    [SerializeField] private float coinFrequency;
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
        dis = ceiling.GetComponent<Renderer>().bounds.size.x;
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

    public void GenerateMap(Vector3 position) {

        GameObject map = Instantiate(mapPrefab);
        map.transform.position = new Vector3(position.x + (spawnDistanceMax * dis), position.y, position.z);

        if (Random.Range(0f, 1f) <= obstacleFrequency) {
            GenerateObstacle(map.transform.position);
        }
        if (Random.Range(0f, 1f) <= coinFrequency) {
            GenerateCoin(map.transform.position);
        }
        //Debug.Log("[ProceduralGeneration] Generating map");
    }

    private void GenerateObstacle(Vector3 position) {
        GameObject obstacle = Instantiate(obstaclePrefab);
        obstacle.transform.position = new Vector3(position.x + Random.Range(0, dis) - (dis / 2), position.y, position.z);

        //obstacle.GetComponent<GravityManager>().
    }

    private void GenerateCoin(Vector3 position) {

        Vector3 newPos = new Vector3(Random.Range(position.x + leftBound, position.x + rightBound),
                                     Random.Range(position.y + lowerBound, position.y + upperBound),
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
