using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnable", menuName = "SpawnableObject", order = 1)]
public class Spawnable : ScriptableObject {

    public GameObject[] objects;

    public int parentSpawnIndexMin;
    public int parentSpawnIndexMax;
    
    public bool gravityOn;
}
