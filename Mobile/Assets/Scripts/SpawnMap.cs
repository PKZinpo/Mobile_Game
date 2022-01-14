using UnityEngine;

public class SpawnMap : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<ProceduralGeneration>().GenerateMap(transform.parent.transform.position);
            Debug.Log("[SpawnMap] Player entered trigger, spawning more map");
        }
    }
}
