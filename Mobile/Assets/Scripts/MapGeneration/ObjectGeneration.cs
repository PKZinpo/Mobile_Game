using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour {

    [SerializeField] private Spawnable spawnable;

    private MapGeneration mgObject;
    private Collider objectCollider;
    private int spawnIndex;
    private int parentSpawnIndexMin;
    private int parentSpawnIndexMax;
    private bool gravityOn;
    private bool gravityInverse;
    private float spawnLeeway = 1f;

    

    private void Start() {
        mgObject = GameObject.Find("MapGeneration").GetComponent<MapGeneration>();
        objectCollider = spawnable.objectToSpawn.GetComponent<Collider>();

        parentSpawnIndexMin = spawnable.parentSpawnIndexMin;
        parentSpawnIndexMax = spawnable.parentSpawnIndexMax;
        gravityOn = spawnable.gravityOn;
        gravityInverse = spawnable.gravityInverse;
    }

    public void SpawnObject(Vector3 position, float xDis, float yDis) {
        GameObject obstacle = Instantiate(spawnable.objectToSpawn);

        float randomxPos = Mathf.Clamp(position.x + Random.Range(0, xDis) - (xDis / 2),
                                       position.x - (xDis / 2) + (objectCollider.bounds.extents.x + spawnLeeway),
                                       position.x + (xDis / 2) - (objectCollider.bounds.extents.x + spawnLeeway));
        float randomyPos = Mathf.Clamp(position.y + Random.Range(0, yDis) - (yDis / 2),
                                       position.y - (yDis / 2) + (objectCollider.bounds.extents.y + spawnLeeway),
                                       position.y + (yDis / 2) - (objectCollider.bounds.extents.y + spawnLeeway));

        //Debug.Log("[ObjectGeneration] Spawn position at " + new Vector3(randomxPos, randomyPos, position.z));

        obstacle.transform.position = new Vector3(randomxPos, randomyPos, position.z);
    }

}
