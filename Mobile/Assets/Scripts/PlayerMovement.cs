using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GlobalVariables gv;
    private GameManager gm;

    private float gravity;
    private Rigidbody rigidBody;
    
    private void Start() {
        gv = GameObject.Find("Global").GetComponent<GlobalVariables>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!gm.GameStarted) return;

        if (Input.GetMouseButton(0)) {
            gravity = -gv.VerticalGravity;
        }
        else {
            gravity = gv.VerticalGravity;
        }

        //if (Mathf.Abs(rigidBody.velocity.y) < gv.MaxVerticalVelocity) {
        rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        //}
            
        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * gv.HorizontalGravity, ForceMode.Acceleration);
        }
    }

    public void StartGamePlayer() {
        rigidBody.AddForce(Vector3.right * gv.StartImpulse, ForceMode.Impulse);
    }
}
