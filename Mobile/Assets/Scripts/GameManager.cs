using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameStarted = false;

    public bool GameStarted { get { return gameStarted; } set { gameStarted = value; } }

    public void StartGame() {
        GameStarted = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().StartGamePlayer();
    }

}
