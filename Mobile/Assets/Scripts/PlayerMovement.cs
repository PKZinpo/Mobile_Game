using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GlobalVariables gv;
    private GameManager gm;
    private GravityManager gravityManager;

    private Rigidbody rigidBody;
    private bool gameOver = true;
    
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
        }
        else {
            gravityManager.InvertGravity = false;
        }

        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * gv.HorizontalGravity, ForceMode.Acceleration);
        }

        //if (rigidBody.velocity.x <= 0f && !gameOver) {
        //    gm.GameEnd();
        //}
    }

    public void StartGamePlayer() {
        rigidBody.AddForce(Vector3.right * gv.StartImpulse, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) {
        //if (collision.gameObject.tag == "Bound" && !gm.GameStarted) rigidBody.velocity = Vector3.zero;
        if (collision.gameObject.tag != "Obstacle") return;

        ICollision collisionObject = collision.gameObject.GetComponent<ICollision>();
        if (collisionObject != null) {
            collisionObject.Collision();
        }
    }

    private void OnTriggerEnter(Collider trigger) {

        ITrigger triggerObject = trigger.gameObject.GetComponent<ITrigger>();
        if (triggerObject != null) {
            triggerObject.Trigger();
        }
    }
}
