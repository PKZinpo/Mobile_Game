using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float gravityVal;
    
    private float gravity;
    private Rigidbody rigidBody;
    
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            //Debug.Log("[PlayerMovement] Gravity Flipped");
            gravity = -gravityVal;
        }
        else {
            gravity = gravityVal;
        }

        rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        //rigidBody.velocity.x = Mathf.Max()
    }
}
