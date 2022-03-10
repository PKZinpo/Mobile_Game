using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInGame : MonoBehaviour {

    private LevelLoader ll;

    private void Start() {
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        ll.OnLoadData += LoadCurrentSkin;
    }

    private void LoadCurrentSkin(object sender, LevelLoader.OnLoadDataEventArgs e) {
        //GameObject playerObject = Instantiate(skinList[currentSkin].skin, GameObject.FindGameObjectWithTag("Player").transform);
        //playerObject.GetComponent<Renderer>().material.SetColor("_MainColor", e.gameData.skinColor);
        ll.OnLoadData -= LoadCurrentSkin;
    }
}
