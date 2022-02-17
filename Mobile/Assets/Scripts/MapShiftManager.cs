using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShiftManager : MonoBehaviour {

    [SerializeField] private float shiftDistance;

    public event EventHandler OnMapShift;

    private GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MapShiftCheck() {
        if (player.transform.position.x > shiftDistance) {
            MapShift();
        }
    }

    private void MapShift() {
        for (int i = 0; i < transform.childCount; i++) {
            Vector3 position = transform.GetChild(i).transform.position;
            transform.GetChild(i).transform.position = new Vector3(position.x - shiftDistance, position.y, position.z);
        }
        Debug.Log("[MapShiftManager] Map shifted to origin");
    }
}
