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
    private int selectedSkinIndex;
    private int skinAmount;
    private float colorIconYSize;


    private void Awake() {
        sm = GameObject.FindGameObjectWithTag("SkinManager").GetComponent<SkinManager>();
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        ll.OnLoadData += LoadSkinColors;
        sm.OnSkinLoad += LoadSelector;

        colorIconYSize = colorIcon.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void LoadSkinColors(object sender, LevelLoader.OnLoadDataEventArgs e) {
        Color[] colors = sm.GetCurrentSkinColors(e.gameData.skinName);
        for (int i = 0; i < colors.Length; i++) {
            colorList.Add(colors[i]);
            GameObject icon = Instantiate(colorIcon, transform);
            icon.GetComponent<Image>().color = sm.SkinColorToUIColor(colorList[i]);

        }
        skinAmount = sm.GetCurrentSkinColors(e.gameData.skinName).Length;
        Debug.Log("[ColorPicker] Loaded current skin's colors");
    }
    private void LoadSelector(object sender, EventArgs e) {
        //for (int i = 0; i < colorList.Count; i++) {
        //    if (colorList[i] == sm.GetCurrentColor()) {
        //        selectedSkinIndex = i + 1;
        //        float moveAmount;
        //        if (skinAmount % 2 == 0) {
        //            int halfSkinAmount = skinAmount / 2;
        //            moveAmount = (halfSkinAmount - selectedSkinIndex) * colorIconYSize + (-colorIconYSize / 2);
        //        }
        //        else {
        //            int halfSkinAmount = skinAmount / 2 + 1;
        //            moveAmount = (halfSkinAmount - selectedSkinIndex) * colorIconYSize;
        //        }
        //        colorSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(colorSelector.GetComponent<RectTransform>().anchoredPosition.x, moveAmount);
        //        break;
        //    }
        //}
        for (int i = 0; i < colorList.Count; i++) {
            if (colorList[i] == sm.GetCurrentColor()) {
                selectedSkinIndex = i + 1;
                break;
            }
        }
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        AdjustButtons();
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
    private void AdjustButtons() {
        if (skinAmount % 2 == 0) {
            int halfSkinAmount = skinAmount / 2;
            float moveAmount = colorIconYSize * halfSkinAmount - (colorIconYSize / 2);
            moveUp.GetComponent<RectTransform>().anchoredPosition = new Vector2(moveUp.GetComponent<RectTransform>().anchoredPosition.x, moveAmount);
            moveDown.GetComponent<RectTransform>().anchoredPosition = new Vector2(moveUp.GetComponent<RectTransform>().anchoredPosition.x, -moveAmount);
        }
        else {
            int halfSkinAmount = skinAmount / 2 + 1;
            float moveAmount = colorIconYSize * halfSkinAmount;
            moveUp.GetComponent<RectTransform>().anchoredPosition = new Vector2(moveUp.GetComponent<RectTransform>().anchoredPosition.x, moveAmount);
            moveDown.GetComponent<RectTransform>().anchoredPosition = new Vector2(moveUp.GetComponent<RectTransform>().anchoredPosition.x, -moveAmount);
        }
    }
    public void MoveSelectorUp() {
        Debug.Log(selectedSkinIndex - 1 + " and " + colorList.Count);
        selectedSkinIndex = Mathf.Clamp(selectedSkinIndex - 1, 1, colorList.Count);
        Debug.Log(selectedSkinIndex);
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        
        //colorSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(colorSelector.GetComponent<RectTransform>().anchoredPosition.x, colorSelector.GetComponent<RectTransform>().anchoredPosition.y + colorIconYSize);
    }
    public void MoveSelectorDown() {
        Debug.Log(selectedSkinIndex - 1 + " and " + colorList.Count);
        selectedSkinIndex = Mathf.Clamp(selectedSkinIndex + 1, 1, colorList.Count);
        Debug.Log(selectedSkinIndex);
        AdjustSelector(colorList[selectedSkinIndex - 1]);
        //colorSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(colorSelector.GetComponent<RectTransform>().anchoredPosition.x, colorSelector.GetComponent<RectTransform>().anchoredPosition.y - colorIconYSize);
    }
}
