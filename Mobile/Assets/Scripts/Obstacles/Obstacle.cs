using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollision {

    public void CheckRotation() {
        if (name.Contains("Tri")) { 
            if (GetComponent<GravityManager>().InvertGravity) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180f);
            }
        }
    }
    public void Collision() {
        GetComponent<Collider>().enabled = false;
    }
}
