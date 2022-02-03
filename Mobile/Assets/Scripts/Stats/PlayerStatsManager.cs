using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour {

    #region Player Stat Variables

    private int playerScore;
    private float playerEnergy;
    private float scoreMultiplier;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public float PlayerEnergy { get { return playerEnergy; } set { playerEnergy = Mathf.Clamp(value, 0f, 100f); } }
    public float ScoreMultiplier { get { return scoreMultiplier; } set { scoreMultiplier = value; } }

    #endregion

    public event EventHandler OnAddScore;
    public event EventHandler OnChangeEnergy;

    [Range(0f, 3f)]
    [SerializeField] private float energyLossRate;
    
    private GameManager gm;


    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gm.OnGameStart += OnStartGamePlayerStats;
    }

    private void Update() {
        if (!gm.GameStarted) return;

        if (PlayerEnergy > 0f) {
            AddEnergy(-energyLossRate);
        }
    }

    private void OnStartGamePlayerStats(object sender, EventArgs e) {
        PlayerScore = 0;
        PlayerEnergy = 100f;
        ScoreMultiplier = 1f;
    }
    public void AddScore(float val) {
        PlayerScore += (int)(val * ScoreMultiplier);
        OnAddScore?.Invoke(this, EventArgs.Empty);
    }
    public void AddEnergy(float val) {
        PlayerEnergy += val;
        OnChangeEnergy?.Invoke(this, EventArgs.Empty);
    }
}
