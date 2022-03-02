using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkin {

    public GameObject skin;
    public Color[] colors;

}
public class SkinManager : MonoBehaviour {

    //[HideInInspector] public string currentSkin;
    //[HideInInspector] public Color currentColor;

    [SerializeField] private PlayerSkin[] skinProperties;

    private Dictionary<string, PlayerSkin> skinList = new Dictionary<string, PlayerSkin>();
    private LevelLoader ll;

    private string currentSkin;
    private Color currentColor;
    private GameObject playerObject;

    private void Awake() {
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        ll.OnLoadData += LoadCurrentSkin;
        
        for (int i = 0; i < skinProperties.Length; i++) {
            skinList.Add(skinProperties[i].skin.name, skinProperties[i]);
            Debug.Log("[Skinmanager] Added skin " + skinProperties[i].skin.name + " to dictionary");
        }
        
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(GetCurrentSkinColors().Length);
        }
    }
    private void LoadCurrentSkin(object sender, LevelLoader.OnLoadDataEventArgs e) {
        currentSkin = e.gameData.skinName;
        currentColor = e.gameData.skinColor;
        playerObject = Instantiate(skinList[currentSkin].skin, GameObject.FindGameObjectWithTag("Player").transform);
        playerObject.GetComponent<Renderer>().material.color = currentColor;
        Debug.Log("[SkinManager] Loaded player in skin selector");
    }
    public void ChangeCurrentSkin() {

    }
    public void ConfirmCurrentSkin() {

    }

    public Color[] GetCurrentSkinColors() {
        return skinList[currentSkin].colors;
    }

}
