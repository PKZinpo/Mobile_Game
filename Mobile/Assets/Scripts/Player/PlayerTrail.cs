using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour {

    private Rigidbody playerRigidbody;

    private void Start() {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void LateUpdate() {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, GetAngle());
    }

    private float GetAngle() {

        float angle = Mathf.Rad2Deg * Mathf.Atan(playerRigidbody.velocity.y / playerRigidbody.velocity.x);
        return angle;
    }
}
