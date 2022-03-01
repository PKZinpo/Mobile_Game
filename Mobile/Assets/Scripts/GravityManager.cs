using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    //[SerializeField] private bool verticalVelocityCap;
    
    private GlobalVariables gv;
    //private GameManager gm;
    private Rigidbody rigidBody;
    private float gravity;
    private bool invertGravity;
    private bool gravityOn;
    private bool movementOn;
    private Vector3 movementVelocity;

    public bool InvertGravity { get { return invertGravity; } set { invertGravity = value; InvertGravityObject(); } }
    public float Gravity { get { return gravity; } set { gravity = value; } }

    private void Awake() {
        gv = GameObject.FindGameObjectWithTag("Global").GetComponent<GlobalVariables>();
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rigidBody = GetComponent<Rigidbody>();

        //InvertGravityObject();
    }
    private void FixedUpdate() {
        if (gravityOn) rigidBody.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);
    }
    #region Object Gravity
   private void InvertGravityObject() {
        Gravity = InvertGravity ? -gv.VerticalGravity : gv.VerticalGravity;
    }
    public void RandomGravity() {
        InvertGravity = Random.Range(0, 2) != 0;
    }
    public void ChangeGravityOn(bool grav) {
        gravityOn = grav;
        InvertGravityObject();
    }
    #endregion

    #region Object Movement
    private void RandomMovement() {
        Gravity = Random.Range(0, 2) != 0 ? gv.ObjectImpulse : -gv.ObjectImpulse;
    }
    public void MovementImpulse() {
        RandomMovement();
        movementOn = true;
        movementVelocity = Vector3.down * Gravity;
        rigidBody.AddForce(movementVelocity, ForceMode.Impulse);
    }
    private void ReverseMovement() {
        movementVelocity = -movementVelocity;
        rigidBody.velocity = movementVelocity;
    }
    #endregion

    #region Collisions and Triggers
    private void OnCollisionEnter(Collision collision) {
        if (!movementOn || !collision.collider.CompareTag("Bound")) return;
        ReverseMovement();
        Debug.Log("[GravityManager] Object bound collision");
    }
    private void OnTriggerEnter(Collider other) {
        if (!movementOn || !other.CompareTag("Bound")) return;
        ReverseMovement();
        Debug.Log("[GravityManager] Object bound collision");
    }
    #endregion
}
