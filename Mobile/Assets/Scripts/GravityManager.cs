using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    [SerializeField] private bool invertGravity;
    [SerializeField] private bool verticalVelocityCap;

    private GlobalVariables gv;
    private GameManager gm;
    private Rigidbody rigidBody;
    private float gravity;

    public bool InvertGravity { get { return invertGravity; } set { invertGravity = value; InvertGravityObject(); } }
    //public float Gravity { get { return gravity; } set { if (value < 0) InvertGravity = false; else InvertGravity = true; gravity = value; } }
    public float Gravity { get { return gravity; } set { gravity = value; } }

    private void Awake() {
        gv = GameObject.Find("Global").GetComponent<GlobalVariables>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        rigidBody = GetComponent<Rigidbody>();

        InvertGravityObject();
    }
    private void FixedUpdate() {
        GravityOnObject();
    }

    private void GravityOnObject() {
        if (tag == "Player" && !gm.GameStarted) return;
        if (verticalVelocityCap) {
            if (InvertGravity) {
                if (rigidBody.velocity.y > -gv.MaxVerticalVelocity) {
                    rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
                }
            }
            else {
                if (rigidBody.velocity.y < gv.MaxVerticalVelocity) {
                    rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
                }
            }
        }
        else {
            rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    public void InvertGravityObject() {
        if (InvertGravity) {
            Gravity = -gv.VerticalGravity;
        }
        else {
            Gravity = gv.VerticalGravity;
        }
    }
    public void RandomGravity() {
        if (Random.Range(0,2) == 0) {
            InvertGravity = false;
        }
        else {
            InvertGravity = true;
        }
    }
}
