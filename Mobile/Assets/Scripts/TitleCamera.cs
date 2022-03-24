using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

    private TitleScreenManager tsm;
    private float rotateSpeed;

    public float RotateSpeed { get { return rotateSpeed; } set { rotateSpeed = value; } }

    private void Awake() {
        tsm = GameObject.Find("TitleScreenManager").GetComponent<TitleScreenManager>();
    }
    private void Update() {
        transform.Rotate(0f, RotateSpeed * Time.deltaTime, 0f);
    }
    private void ToggleTitleUI() {
        tsm.ToggleUI();
    }
}
