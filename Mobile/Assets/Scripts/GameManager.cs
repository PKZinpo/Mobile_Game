using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool gameStarted = false;
    private bool resettingGame = false;

    public bool GameStarted { get { return gameStarted; } set { gameStarted = value; } }

    public void StartGame() {
        GameStarted = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().StartGamePlayer();
    }
    public void GameEnd() {
        if (resettingGame) return;
        Debug.Log("[GameManager] Resetting game");
        resettingGame = true;
        GameStarted = false;
        Invoke("ResetGame", 5f);
    }
    private void ResetGame() {
        Debug.Log("[GameManager] Game reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
