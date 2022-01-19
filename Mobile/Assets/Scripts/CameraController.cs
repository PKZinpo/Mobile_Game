using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float zoomScale;
    [SerializeField] private float baseCameraSize;
    [SerializeField] private float smoothCameraSizeSpeed;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;
    [SerializeField] private float leftBound;

    private Camera playerCamera;
    private Rigidbody rigidBody;

    private void Awake() {
        playerCamera = GetComponent<Camera>();
        rigidBody = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        
    }
    private void LateUpdate() {
        playerCamera.orthographicSize = CameraSizeChange(playerCamera.orthographicSize);

        transform.position = player.transform.position + offset + VerticalBoundCheck() + HorizontalBoundCheck();
    }

    private float CameraSizeChange(float size) { // Function to change camera size according to magnitude of player object velocity

        float targetSize = baseCameraSize + (zoomScale * rigidBody.velocity.x);
        float newSize = Mathf.Lerp(size, targetSize, smoothCameraSizeSpeed * Time.deltaTime);

        return newSize;
    }

    private Vector3 VerticalBoundCheck() {

        float topPos = Mathf.Min(topBound - playerCamera.orthographicSize, player.transform.position.y);
        float botPos = Mathf.Max(bottomBound + playerCamera.orthographicSize, player.transform.position.y);

        Vector3 verticalOffset = new Vector3(0f, Mathf.Clamp(player.transform.position.y, botPos, topPos) - player.transform.position.y, 0f);

        return verticalOffset;
    }

    private Vector3 HorizontalBoundCheck() {

        float halfWidth = playerCamera.orthographicSize * playerCamera.aspect;
        float leftPos = halfWidth - leftBound;

        Vector3 horizontalOffset = new Vector3(leftPos, 0f, 0f);

        return horizontalOffset;
    }
}
