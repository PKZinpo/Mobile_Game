using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour {

    [SerializeField] private Spawnable spawnable;

    private MapGeneration mgObject;
    private int spawnIndex;
    private int targetSpawnIndex;
    private int spawnedIndex;
    private float spawnLeeway = 1f;

    private Transform mapParentTransform;

    private void Start() {
        mgObject = GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<MapGeneration>();
        mapParentTransform = GameObject.FindGameObjectWithTag("MapParent").transform;

        if (spawnable.parentSpawnIndexMin == 0 && spawnable.parentSpawnIndexMax == 0) {
            targetSpawnIndex = 0;
        }
        else {
            ResetSpawnIndex();
        }

        spawnedIndex = 0;
    }

    public void ObjectPrepareForQueue() {
        if (targetSpawnIndex != 0) {
            if (transform.parent.GetComponent<ObjectGeneration>().GetSpawnIndex() - spawnedIndex >= targetSpawnIndex) {
                mgObject.AddToSpawnQueue(SpawnObject);
                ResetSpawnIndex();
                ChildObjectPrepareQueue();
                spawnIndex++;
                spawnedIndex = transform.parent.GetComponent<ObjectGeneration>().GetSpawnIndex();
            }
        }
        else {
            mgObject.AddToSpawnQueue(SpawnObject);
            ChildObjectPrepareQueue();
            spawnIndex++;
        }
    }

    public void SpawnObject(Vector3 position, float xDis, float yDis) {

        GameObject objectToSpawn = spawnable.objects[Random.Range(0, spawnable.objects.Length)];
        GameObject mapObject = Instantiate(objectToSpawn, mapParentTransform);

        Renderer objectRenderer = objectToSpawn.GetComponent<Renderer>();

        float randomxPos = Mathf.Clamp(position.x + Random.Range(0, xDis) - (xDis / 2),
                                       position.x - (xDis / 2) + (objectRenderer.bounds.extents.x + spawnLeeway),
                                       position.x + (xDis / 2) - (objectRenderer.bounds.extents.x + spawnLeeway));
        float randomyPos = Mathf.Clamp(position.y + Random.Range(0, yDis) - (yDis / 2),
                                       position.y - (yDis / 2) + (objectRenderer.bounds.extents.y + spawnLeeway),
                                       position.y + (yDis / 2) - (objectRenderer.bounds.extents.y + spawnLeeway));

        //Debug.Log("[ObjectGeneration] " + spawnable.objectToSpawn.name + "Spawn position at " + new Vector3(randomxPos, randomyPos, position.z));

        mapObject.transform.position = new Vector3(randomxPos, randomyPos, position.z);

        #region Gravity
        if (spawnable.randomizeGravityOn) {
            spawnable.gravityOn = Random.Range(0, 2) == 0 ? false : true;
            mapObject.GetComponent<GravityManager>().ChangeGravityOn(mapObject.name.Contains("Tri") ? true : spawnable.gravityOn);
        }
        if (spawnable.gravityOn) {
            mapObject.GetComponent<GravityManager>().RandomGravity();
        }
        #endregion

        if (mapObject.tag == "Obstacle") {
            mapObject.GetComponent<Obstacle>().CheckRotation();
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
