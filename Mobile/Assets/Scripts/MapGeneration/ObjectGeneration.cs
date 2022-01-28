using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour {

    [SerializeField] private Spawnable spawnable;

    private MapGeneration mgObject;
    private Renderer objectRenderer;
    private int spawnIndex;
    private int targetSpawnIndex;
    private float spawnLeeway = 1f;

    private Transform mapParentTransform;

    private void Start() {
        mgObject = GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<MapGeneration>();
        objectRenderer = spawnable.objectToSpawn.GetComponent<Renderer>();
        mapParentTransform = GameObject.FindGameObjectWithTag("MapParent").transform;

        if (spawnable.parentSpawnIndexMin == 0 && spawnable.parentSpawnIndexMax == 0) {
            targetSpawnIndex = 0;
        }
        else {
            ResetSpawnIndex();
        }
    }

    public void ObjectPrepareForQueue() {
        spawnIndex++;
        if (targetSpawnIndex != 0) {
            if (transform.parent.GetComponent<ObjectGeneration>().GetSpawnIndex() % targetSpawnIndex == 0) {
                mgObject.AddToSpawnQueue(SpawnObject);
                ResetSpawnIndex();
                ChildObjectPrepareQueue();
            }
        }
        else {
            mgObject.AddToSpawnQueue(SpawnObject);
            ChildObjectPrepareQueue();
        }
    }

    public void SpawnObject(Vector3 position, float xDis, float yDis) {
        GameObject obstacle = Instantiate(spawnable.objectToSpawn, mapParentTransform);

        float randomxPos = Mathf.Clamp(position.x + Random.Range(0, xDis) - (xDis / 2),
                                       position.x - (xDis / 2) + (objectRenderer.bounds.extents.x + spawnLeeway),
                                       position.x + (xDis / 2) - (objectRenderer.bounds.extents.x + spawnLeeway));
        float randomyPos = Mathf.Clamp(position.y + Random.Range(0, yDis) - (yDis / 2),
                                       position.y - (yDis / 2) + (objectRenderer.bounds.extents.y + spawnLeeway),
                                       position.y + (yDis / 2) - (objectRenderer.bounds.extents.y + spawnLeeway));

        //Debug.Log("[ObjectGeneration] " + spawnable.objectToSpawn.name + "Spawn position at " + new Vector3(randomxPos, randomyPos, position.z));

        obstacle.transform.position = new Vector3(randomxPos, randomyPos, position.z);

        if (spawnable.gravityOn) {
            obstacle.GetComponent<GravityManager>().RandomGravity();
        }
        
    }
    public int GetSpawnIndex() {
        return spawnIndex;
    }
    private void ChildObjectPrepareQueue() {
        if (transform.childCount > 0) {
            transform.GetChild(0).GetComponent<ObjectGeneration>().ObjectPrepareForQueue();
        }
    }
    private void ResetSpawnIndex() {
        targetSpawnIndex = Random.Range(spawnable.parentSpawnIndexMin, spawnable.parentSpawnIndexMax);
        spawnIndex = 0;
    }

}
