using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    private float despawnDis = 60f;

    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        float xdistance = player.transform.position.x - transform.position.x;
        float ydistance = player.transform.position.y - transform.position.y;

        if (xdistance > despawnDis || Mathf.Abs(ydistance) > despawnDis) {
            //Debug.Log("[Despawn] Object despawned at " + distance);
            Destroy(gameObject);
        }
    }
}
