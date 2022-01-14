using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float verticalGravity;
    [SerializeField] private float horizontalGravity;
    [SerializeField] private float horizontalImpulse;
    
    private float gravity;
    private Rigidbody rigidBody;
    
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            gravity = -verticalGravity;
        }
        else {
            gravity = verticalGravity;
        }

        rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * horizontalGravity, ForceMode.Acceleration);
        }
        
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
