using UnityEngine;

public class SpawnMap : MonoBehaviour, ITrigger {
    public void Trigger() {
        GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<MapGeneration>().GenerateMap(transform.parent.transform.position);
        //Debug.Log("[SpawnMap] Player entered trigger, spawning more map");
    }
}
