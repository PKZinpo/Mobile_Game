using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GlobalVariables gv;
    private GameManager gm;
    private GravityManager gravityManager;
    private PlayerStatsManager psm;

    private Rigidbody rigidBody;
    //private bool gameOver = true;
    
    private void Start() {
        gv = GameObject.FindGameObjectWithTag("Global").GetComponent<GlobalVariables>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        psm = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>();

        gravityManager = GetComponent<GravityManager>();
        rigidBody = GetComponent<Rigidbody>();

        gm.OnGameStart += OnStartGamePlayer;
        gm.OnGameEnd += OnEndGamePlayer;
    }

    private void FixedUpdate() {
        if (!gm.GameStarted || gm.GameEnded) return;

        if (Input.GetMouseButton(0)) {
            gravityManager.InvertGravity = true;
        }
        else {
            gravityManager.InvertGravity = false;
        }

        if (rigidBody.velocity.x > 0f) {
            rigidBody.AddForce(Vector3.left * gv.HorizontalGravity, ForceMode.Acceleration);
            psm.AddScore(rigidBody.velocity.x);
        }

    }

    private void OnStartGamePlayer(object sender, EventArgs e) {
        rigidBody.AddForce(Vector3.right * gv.StartImpulse, ForceMode.Impulse);
    }

    private void OnEndGamePlayer(object sender, EventArgs e) {
        rigidBody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision) {
        if (gm.GameEnded) return;

        ICollision collisionObject = collision.gameObject.GetComponent<ICollision>();
        if (collisionObject != null) {
            collisionObject.Collision();
        }
    }

    private void OnTriggerEnter(Collider trigger) {
        if (gm.GameEnded) return;

        ITrigger triggerObject = trigger.gameObject.GetComponent<ITrigger>();
        if (triggerObject != null) {
            triggerObject.Trigger();
        }
    }

    private void OnDestroy() {
        gm.OnGameStart -= OnStartGamePlayer;
        gm.OnGameEnd -= OnEndGamePlayer;
    }
}
