using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public event EventHandler<OnLoadDataEventArgs> OnLoadData;
    public event EventHandler OnSkinLoad;

    public class OnLoadDataEventArgs : EventArgs {
        public GameSaveData gameData;
    }

    private void Start() {
        LoadData();
    }

    public void LoadData() {

        SaveData data = SaveSystem.LoadGameData();
        GameSaveData game = FindObjectOfType<GameSaveData>();

        if (data == null) {
            game.highscore = 0;
            game.skinName = "Original";
            game.skinColor = new Color(0f, 150/255f, 0f, 255/255f);
            game.musicOff = false;
            game.soundOff = false;
            Debug.Log("[LevelLoader] No game data exists, creating new data");
        }
        else {
            game.highscore = data.highscore;
            game.skinName = data.skinName;
            game.skinColor = new Color(data.colors[0], data.colors[1], data.colors[2], data.colors[3]);
            game.musicOff = data.musicOff;
            game.soundOff = data.soundOff;
            Debug.Log("[LevelLoader] Loaded game data");
        }
        OnLoadData?.Invoke(this, new OnLoadDataEventArgs { gameData = game });
        OnSkinLoad?.Invoke(this, EventArgs.Empty);
    }
    private void SaveData() {
        SaveSystem.SaveGameData(FindObjectOfType<GameSaveData>());
        Debug.Log("[LevelLoader] Saved game data");
    }
    public void LoadScene(string sceneName) {
        SaveData();
        SceneManager.LoadScene(sceneName);
    }
    public void OnApplicationQuit() {
        SaveData();
    }

}
