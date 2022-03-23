using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

    private float rotateSpeed;

    public float RotateSpeed { get { return rotateSpeed; } set { rotateSpeed = value; } }

    void Update() {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
