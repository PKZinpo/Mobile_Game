using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    [SerializeField] private float horizontalImpulse;

    private GlobalVariables gv;

    private float gravity;
    private Rigidbody rigidBody;
    
    private void Start() {
        gv = GameObject.Find("Global").GetComponent<GlobalVariables>();
        Debug.Log("[PlayerMovement] Gravity is " + gv.VerticalGravity + " and " + gv.HorizontalGravity);
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            gravity = -gv.VerticalGravity;
        }
        else {
            gravity = gv.VerticalGravity;
        }

        rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * gv.HorizontalGravity, ForceMode.Acceleration);
        }
        //if (rigidBody.velocity.x < 10f) {
        //    rigidBody.AddForce(Vector3.right * horizontalImpulse, ForceMode.Force);
        //}
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            rigidBody.AddForce(Vector3.right * horizontalImpulse, ForceMode.Impulse);
        }
        //if (Input.touchCount > 0) {
        //    Touch touch = Input.GetTouch(0);
        //    Debug.Log(touch.position);
        //}
    }
}
