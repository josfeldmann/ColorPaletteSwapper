using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PaletteSwapping {


    public class PaletteColorMenu : MonoBehaviour {
        public ColorSelectButton colorSelectPrefab;
        public Transform sortSpot;
        public List<ColorSelectButton> selectButtons = new List<ColorSelectButton>();
        ColorPalette currentPalette;
        public ColorPicker picker;
        public CustomPaletteCreator creator;


        public void ClearList() {
            foreach (ColorSelectButton c in selectButtons) {
                Destroy(c.gameObject);
            }
            selectButtons.Clear();
        }

        internal void SelectButton(ColorSelectButton colorSelectButton) {

            currentlySelectedIndex = colorSelectButton.index;
            picker.CurrentColor = colorSelectButton.image.color;

        }

        public int currentlySelectedIndex = 0;

        public void init(ColorPalette c) {

            ClearList();
            int index = 0;
            currentPalette = c;
            foreach (Color a in c.colors) {
                ColorSelectButton c1 = Instantiate(colorSelectPrefab, sortSpot);
                c1.init(this, index, a);
                index++;
                selectButtons.Add(c1);
            }
            picker.gameObject.SetActive(true);
            currentlySelectedIndex = 0;
            picker.CurrentColor = c.colors[0];



        }

        public void Close() {
            picker.gameObject.SetActive(false);
        }

        public void ColorChanged(Color c) {
            if (selectButtons != null &&selectButtons.Count > 0)
            selectButtons[currentlySelectedIndex].SetColor(c);
            if (currentPalette != null && currentPalette.colors.Length > 0)currentPalette.colors[currentlySelectedIndex] = c;
            creator.UpdateDemoBasedOnPanels();
        }
    }

}
