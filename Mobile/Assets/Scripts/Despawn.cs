using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

    private float despawnDis = 50f;

    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > despawnDis && transform.position.x - player.transform.position.x < 0) {
            //Debug.Log("[Despawn] Object despawned at " + distance);
            Destroy(gameObject);
        }
    }
}
