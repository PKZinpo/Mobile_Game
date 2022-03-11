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

    [SerializeField] private PlayerSkin[] skinProperties;
    [SerializeField] private Color[] colorUI;
    [SerializeField] private Color[] colorSkin;

    private Dictionary<string, PlayerSkin> skinList = new Dictionary<string, PlayerSkin>();
    private LevelLoader ll;

    private string currentSkin;
    private Color currentColor;
    private GameObject playerObject;

    //private static SkinManager instance;

    private void Awake() {

        //if (instance != null) {
        //    Destroy(gameObject);
        //    return;
        //}
        //instance = this;
        //DontDestroyOnLoad(gameObject);

        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        ll.OnLoadData += LoadCurrentSkin;
        
        for (int i = 0; i < skinProperties.Length; i++) {
            skinList.Add(skinProperties[i].skin.name, skinProperties[i]);
            Debug.Log("[Skinmanager] Added skin " + skinProperties[i].skin.name + " to dictionary");
        }
    }

    private void LoadCurrentSkin(object sender, LevelLoader.OnLoadDataEventArgs e) {
        currentSkin = e.gameData.skinName;
        currentColor = e.gameData.skinColor;
        if (playerObject == null) playerObject = Instantiate(skinList[currentSkin].skin, GameObject.FindGameObjectWithTag("Player").transform);
        playerObject.GetComponent<Renderer>().material.SetColor("_MainColor", currentColor);
        playerObject.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material.SetColor("_MainColor", currentColor);
        Debug.Log("[SkinManager] Loaded player");
    }
    public void ChangeCurrentColor(Color color) {
        currentColor = color;
        playerObject.GetComponent<Renderer>().material.SetColor("_MainColor", currentColor);
    }
    public void ConfirmCurrentSkin() {
        FindObjectOfType<GameSaveData>().skinName = currentSkin;
        FindObjectOfType<GameSaveData>().skinColor = currentColor;
        Debug.Log("[SkinManager] Set new skin to " + currentSkin + " and " + currentColor);
    }
    public Color GetCurrentColor() {
        return currentColor;
    }
    public Color[] GetCurrentSkinColors(string skin) {
        return skinList[skin].colors;
    }

    public Color UIColorToSkinColor(Color colorIn) {
        for (int i = 0; i < colorUI.Length; i++) {
            if (colorUI[i] == colorIn) {
                return colorSkin[i];
            }
        }
        return Color.white;
    }

    public Color SkinColorToUIColor(Color colorIn) {
        for (int i = 0; i < colorSkin.Length; i++) {
            if (colorSkin[i] == colorIn) {
                return colorUI[i];
            }
        }
        return Color.white;
    }
}
