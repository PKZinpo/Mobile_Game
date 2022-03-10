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

    public event EventHandler OnSkinLoad;

    [SerializeField] private PlayerSkin[] skinProperties;
    [SerializeField] private Color[] colorUI;
    [SerializeField] private Color[] colorSkin;

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

    private void LoadCurrentSkin(object sender, LevelLoader.OnLoadDataEventArgs e) {
        currentSkin = e.gameData.skinName;
        currentColor = e.gameData.skinColor;
        playerObject = Instantiate(skinList[currentSkin].skin, GameObject.FindGameObjectWithTag("Player").transform);
        playerObject.GetComponent<Renderer>().material.color = currentColor;
        Debug.Log("[SkinManager] Loaded player in skin selector");
        
        OnSkinLoad?.Invoke(this, EventArgs.Empty);
    }
    public void ChangeCurrentSkin() {

    }
    public void ConfirmCurrentSkin() {

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
        //Color colorOut = colorSkin[Array.IndexOf(colorUI, colorIn)];
        return Color.white;
    }

    public Color SkinColorToUIColor(Color colorIn) {
        for (int i = 0; i < colorSkin.Length; i++) {
            if (colorSkin[i] == colorIn) {
                return colorUI[i];
            }
        }
        //Color colorOut = colorUI[Array.IndexOf(colorSkin, colorIn)];
        //Debug.Log("[SkinManager] UI Color out is " + colorOut);
        return Color.white;
    }
}
