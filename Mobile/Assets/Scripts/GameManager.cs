using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool gameStarted = false;
    private bool gameEnded = false;

    public bool GameStarted { get { return gameStarted; } set { gameStarted = value; } }
    public bool GameEnded { get { return gameEnded; } set { gameEnded = value; } }

    public event EventHandler OnGameStart;
    public event EventHandler OnGameEnd;

    public void StartGame() {
        OnGameStart?.Invoke(this, EventArgs.Empty);
        GameStarted = true;
    }
    public void GameEnd() {
        Debug.Log("[GameManager] Resetting game");
        OnGameEnd?.Invoke(this, EventArgs.Empty);
        GameEnded = true;
        Invoke("ResetGame", 5f);
    }
    private void ResetGame() {
        Debug.Log("[GameManager] Game reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
