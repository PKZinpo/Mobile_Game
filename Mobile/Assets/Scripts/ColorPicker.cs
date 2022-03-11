using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {

    [SerializeField] private GameObject colorIcon;
    [SerializeField] private GameObject colorSelector;
    [SerializeField] private GameObject moveUp;
    [SerializeField] private GameObject moveDown;

    private List<Color> colorList = new List<Color>();
    private LevelLoader ll;
    private SkinManager sm;
    private string selectedSkinName;
    private int selectedSkinIndex;
    private int skinAmount;
    private float colorIconYSize;


    private void Awake() {
        sm = GameObject.FindGameObjectWithTag("SkinManager").GetComponent<SkinManager>();
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        ll.OnLoadData += LoadSkinColors;
        ll.OnSkinLoad += LoadSelector;

        colorIconYSize = colorIcon.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void LoadSkinColors(object sender, LevelLoader.OnLoadDataEventArgs e) {
        selectedSkinName = e.gameData.skinName;
        Color[] colors = sm.GetCurrentSkinColors(selectedSkinName);
        for (int i = 0; i < colors.Length; i++) {
            colorList.Add(colors[i]);
            GameObject icon = Instantiate(colorIcon, transform);
            icon.GetComponent<Image>().color = sm.SkinColorToUIColor(colorList[i]);

        }
        skinAmount = sm.GetCurrentSkinColors(selectedSkinName).Length;
        Debug.Log("[ColorPicker] Loaded current skin's colors");
    }
    private void LoadSelector(object sender, EventArgs e) {
        for (int i = 0; i < colorList.Count; i++) {
            if (colorList[i] == sm.GetCurrentColor()) {
                selectedSkinIndex = i + 1;
                break;
            }
        }
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        Debug.Log("[ColorPicker] Loaded selector");
    }
    private void AdjustSelector(Color color) {
        for (int i = 0; i < colorList.Count; i++) {
            if (colorList[i] == color) {
                float moveAmount;
                if (skinAmount % 2 == 0) {
                    int halfSkinAmount = skinAmount / 2;
                    moveAmount = (halfSkinAmount - selectedSkinIndex) * colorIconYSize + (-colorIconYSize / 2);
                }
                else {
                    int halfSkinAmount = skinAmount / 2 + 1;
                    moveAmount = (halfSkinAmount - selectedSkinIndex) * colorIconYSize;
                }
                colorSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(colorSelector.GetComponent<RectTransform>().anchoredPosition.x, moveAmount);
                break;
            }
        }
    }
    public void MoveSelectorUp() {
        selectedSkinIndex = Mathf.Clamp(selectedSkinIndex - 1, 1, colorList.Count);
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        sm.ChangeCurrentColor(colorList[selectedSkinIndex - 1]);
    }
    public void MoveSelectorDown() {
        selectedSkinIndex = Mathf.Clamp(selectedSkinIndex + 1, 1, colorList.Count);
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        sm.ChangeCurrentColor(colorList[selectedSkinIndex - 1]);
    }
    public void SetSkin() {
        sm.ConfirmCurrentSkin();
    }
}
