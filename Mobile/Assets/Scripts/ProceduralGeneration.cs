using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour {

    [SerializeField] private int spawnDistanceMax;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject mapPrefab;

    private float dis;
    private int spawnDistance;
    private GameObject player;


    private void Awake() {
        dis = ceiling.GetComponent<Renderer>().bounds.size.x;
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        for (spawnDistance = 0; spawnDistance < spawnDistanceMax; spawnDistance++) {
            Vector3 targetPos = new Vector3(player.transform.position.x + (spawnDistance * dis), player.transform.position.y, player.transform.position.z);
            if (Physics.Raycast(targetPos, transform.TransformDirection(Vector3.down), out RaycastHit floorHit, Mathf.Infinity)) {
                Debug.DrawRay(targetPos, transform.TransformDirection(Vector3.down) * floorHit.distance);
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

        Debug.Log("[ProceduralGeneration] Generating map");
    }
}
