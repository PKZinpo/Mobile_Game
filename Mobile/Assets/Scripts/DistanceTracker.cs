using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour {

    [SerializeField] private GameObject player;

    private TextMeshProUGUI distanceText;
    private float startPos;
    private float currentPos;

    private void Start() {
        distanceText = GetComponent<TextMeshProUGUI>();
        startPos = player.transform.position.x;
    }

    private void LateUpdate() {
        currentPos = player.transform.position.x;
        distanceText.text = ((int)(currentPos - startPos)).ToString();
    }
}
