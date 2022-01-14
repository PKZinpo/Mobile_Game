using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float zoomScale;
    [SerializeField] private float baseCameraSize;
    [SerializeField] private float smoothCameraSizeSpeed;

    private Camera playerCamera;
    private Rigidbody rigidBody;

    private void Awake() {
        playerCamera = GetComponent<Camera>();
        rigidBody = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        playerCamera.orthographicSize = CameraSizeChange(playerCamera.orthographicSize);

        transform.position = player.transform.position + offset;
    }

    private float CameraSizeChange(float size) { // Function to change camera size according to magnitude of player object velocity

        float targetSize = baseCameraSize + (zoomScale * rigidBody.velocity.magnitude);
        float newSize = Mathf.Lerp(size, targetSize, smoothCameraSizeSpeed * Time.deltaTime);

        return newSize;
    }
}
