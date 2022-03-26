using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

    [SerializeField] private GameObject skinSelectionUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private Animator titleCameraAnimator;
    [SerializeField] private float cameraRotateSpeed;

    private LevelLoader ll;
    private bool inSkinSelection;

    private void Start() {
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        inSkinSelection = false;
        skinSelectionUI.SetActive(false);
    }

    public void ChangeUI() {
        if (inSkinSelection) {
            inSkinSelection = false;
            skinSelectionUI.SetActive(false);
            titleCameraAnimator.SetTrigger("ToTitle");
        }
        else {
            inSkinSelection = true;
            titleUI.SetActive(false);
            ll.LoadData();
            titleCameraAnimator.SetTrigger("ToSkin");
        }
    }
    public void ToggleUI() {
        if (inSkinSelection) {
            skinSelectionUI.SetActive(true);
            GameObject.Find("CameraRotator").GetComponent<TitleCamera>().RotateSpeed = cameraRotateSpeed;
        }
        else {
            titleUI.SetActive(true);
            ll.SaveData();
            GameObject.Find("CameraRotator").GetComponent<TitleCamera>().RotateSpeed = 0f;
        }
        titleCameraAnimator.SetTrigger("ToIdle");
    }
}
