using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GlobalVariables gv;
    private GameManager gm;
    private GravityManager gravityManager;

    private Rigidbody rigidBody;
    
    private void Start() {
        gv = GameObject.Find("Global").GetComponent<GlobalVariables>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        gravityManager = GetComponent<GravityManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!gm.GameStarted) return;

        if (Input.GetMouseButton(0)) {
            gravityManager.InvertGravity = true;
            //gravityManager.Gravity = -gv.VerticalGravity;
        }
        else {
            gravityManager.InvertGravity = false;
            //gravityManager.Gravity = gv.VerticalGravity;
        }

        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * gv.HorizontalGravity, ForceMode.Acceleration);
        }
    }

    public void StartGamePlayer() {
        rigidBody.AddForce(Vector3.right * gv.StartImpulse, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Obstacle") return;

        ICollision collisionObject = collision.gameObject.GetComponent<ICollision>();
        if (collisionObject != null) {
            collisionObject.Collision();
        }
    }
}
