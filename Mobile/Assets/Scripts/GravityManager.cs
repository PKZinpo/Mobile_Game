using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    [SerializeField] private bool verticalVelocityCap;
    [SerializeField] private bool gravityOn;

    private GlobalVariables gv;
    //private GameManager gm;
    private Rigidbody rigidBody;
    private float gravity;
    private bool invertGravity;

    public bool InvertGravity { get { return invertGravity; } set { invertGravity = value; InvertGravityObject(); } }
    public float Gravity { get { return gravity; } set { gravity = value; } }

    private void Awake() {
        gv = GameObject.FindGameObjectWithTag("Global").GetComponent<GlobalVariables>();
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rigidBody = GetComponent<Rigidbody>();

        InvertGravityObject();
    }
    private void FixedUpdate() {
        if (gravityOn) rigidBody.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
    }

    //private void GravityOnObject() {
    //    //if (tag == "Player" && !gm.GameStarted) return;
    //    //if (verticalVelocityCap) {
    //    //    if (InvertGravity) {
    //    //        if (rigidBody.velocity.y < gv.MaxVerticalVelocity) {
    //    //            rigidBody.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
    //    //        }
    //    //    }
    //    //    else {
    //    //        if (rigidBody.velocity.y > -gv.MaxVerticalVelocity) {
    //    //            rigidBody.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
    //    //        }
    //    //    }
    //    //}
    //    //else {
    //        rigidBody.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
    //    //}
    //}

    public void InvertGravityObject() {
        Gravity = InvertGravity ? -gv.VerticalGravity : gv.VerticalGravity;
    }
    public void RandomGravity() {
        InvertGravity = Random.Range(0, 2) == 0 ? false : true;
    }
    public void ChangeGravityOn(bool grav) {
        gravityOn = grav;
    }
}
