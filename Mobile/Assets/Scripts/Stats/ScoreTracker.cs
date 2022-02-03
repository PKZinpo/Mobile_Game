using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour {

    private PlayerStatsManager psm;
    private TextMeshProUGUI scoreText;

    private void Start() {
        psm = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>();

        scoreText = GetComponent<TextMeshProUGUI>();

        psm.OnAddScore += OnScoreUpdate;
    }

    private void OnScoreUpdate(object sender, EventArgs e) {
        scoreText.text = psm.PlayerScore.ToString();
    }

    private void OnDestroy() {
        psm.OnAddScore -= OnScoreUpdate;
    }
}
