using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnable", menuName = "SpawnableObject", order = 1)]
public class Spawnable : ScriptableObject {

    public GameObject objectToSpawn;

    public int parentSpawnIndexMin;
    public int parentSpawnIndexMax;
    
    public bool gravityOn;
    public bool gravityInverse;

    private int spawnIndex;
}
