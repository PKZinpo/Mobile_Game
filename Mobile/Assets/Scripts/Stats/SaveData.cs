using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public int highscore;
    public string skinName;
    public float[] colors;
    public bool soundOff;
    public bool musicOff;

    public SaveData(GameSaveData data) {

        highscore = data.highscore;
        skinName = data.skinName;

        colors = new float[3];
        colors[0] = data.skinColor.r;
        colors[1] = data.skinColor.g;
        colors[2] = data.skinColor.b;

        soundOff = data.soundOff;
        musicOff = data.musicOff;
    }
}
