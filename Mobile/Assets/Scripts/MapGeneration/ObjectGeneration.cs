using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour {

    [SerializeField] private Spawnable spawnable;

    private MapGeneration pgObject;
    private Collider objectCollider;
    private int spawnIndex;
    private int parentSpawnIndexMin;
    private int parentSpawnIndexMax;
    private bool gravityOn;
    private bool gravityInverse;

    

    private void Start() {
        pgObject = GameObject.Find("MapGeneration").GetComponent<MapGeneration>();
        objectCollider = spawnable.objectToSpawn.GetComponent<Collider>();

        parentSpawnIndexMin = spawnable.parentSpawnIndexMin;
        parentSpawnIndexMax = spawnable.parentSpawnIndexMax;
        gravityOn = spawnable.gravityOn;
        gravityInverse = spawnable.gravityInverse;

    }

    public void SpawnObject(Vector3 position, float dis) {
        GameObject obstacle = Instantiate(spawnable.objectToSpawn);
        obstacle.transform.position = new Vector3(position.x + Random.Range(0, dis) - (dis / 2), position.y, position.z);
    }

}
