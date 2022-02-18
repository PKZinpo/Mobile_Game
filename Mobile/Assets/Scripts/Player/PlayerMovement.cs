using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GlobalVariables gv;
    private GameManager gm;
    private PlayerStatsManager psm;

    private Rigidbody rigidBody;
    private float gravity;
    private bool invertGravity;
    //private bool gameOver = true;

    private void Start() {
        gv = GameObject.FindGameObjectWithTag("Global").GetComponent<GlobalVariables>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        psm = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>();

        rigidBody = GetComponent<Rigidbody>();

        gm.OnGameStart += OnStartGamePlayer;
        gm.OnGameEnd += OnEndGamePlayer;
    }

    private void FixedUpdate() {
        if (!gm.GameStarted || gm.GameEnded) return;

        if (Input.GetMouseButton(0)) {
            InvertPlayerGravityObject(true);
        }
        else {
            InvertPlayerGravityObject(false);
        }

        if (invertGravity) {
            if (rigidBody.velocity.y < gv.MaxVerticalVelocity) {
                rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            }
        }
        else {
            if (rigidBody.velocity.y > -gv.MaxVerticalVelocity) {
                rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            }
        }

        if (rigidBody.velocity.x < gv.MaxHorizontalVelocity) {
            rigidBody.AddForce(Vector3.right * gv.HorizontalAcceleration, ForceMode.Acceleration);
        }

        if (!gm.GameEnded) {
            psm.AddScore(rigidBody.velocity.x);
        }

    }
    private void InvertPlayerGravityObject(bool inverted) {
        invertGravity = inverted;
        gravity = invertGravity ? -gv.VerticalGravity : gv.VerticalGravity;
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
