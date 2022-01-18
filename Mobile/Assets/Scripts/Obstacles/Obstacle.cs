using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollision {

    private void Start() {
        GetComponent<GravityManager>().RandomGravity();
    }
    public void Collision() {
        GetComponent<Collider>().enabled = false;
        Invoke("DestroyObstacle", 5f);
    }

    private void DestroyObstacle() {
        Destroy(gameObject);
    }
}
