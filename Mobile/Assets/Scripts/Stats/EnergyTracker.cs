using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyTracker : MonoBehaviour {

    private PlayerStatsManager psm;
    private RectTransform energyBar;

    private void Start() {
        psm = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>();

        energyBar = transform.GetChild(0).GetComponent<RectTransform>();

        psm.OnChangeEnergy += OnEnergyUpdate;
    }

    private void OnEnergyUpdate(object sender, EventArgs e) {
        Vector3 newScale = new Vector3(psm.PlayerEnergy / 100f, 1f, 1f);
        energyBar.localScale = newScale;
    }

    private void OnDestroy() {
        psm.OnChangeEnergy -= OnEnergyUpdate;
    }
}
