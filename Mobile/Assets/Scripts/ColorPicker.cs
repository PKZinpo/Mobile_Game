using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {

    [SerializeField] private GameObject colorIcon;
    [SerializeField] private GameObject colorSelector;
    private LevelLoader ll;
    private SkinManager sm;

    private void Awake() {
        sm = GameObject.FindGameObjectWithTag("SkinManager").GetComponent<SkinManager>();
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        ll.OnLoadData += LoadSkinColors;
        sm.OnSkinLoad += LoadSelector;
    }

    private void LoadSkinColors(object sender, LevelLoader.OnLoadDataEventArgs e) {
        for (int i = 0; i < sm.GetCurrentSkinColors(e.gameData.skinName).Length; i++) {
            GameObject icon = Instantiate(colorIcon, transform);
            icon.GetComponent<Image>().color = sm.SkinColorToUIColor(sm.GetCurrentSkinColors(e.gameData.skinName)[i]);
        }
        Debug.Log("[ColorPicker] Loaded current skin colors");
    }
    private void LoadSelector(object sender, EventArgs e) {

    }
}
