using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PaletteSwapping {
    public class ColorSelectButton : MonoBehaviour {
        public Image image;
        public PaletteColorMenu paletteMenu;
        public int index;

        public void init(PaletteColorMenu m, int id, Color c) {
            index = id;
            paletteMenu = m;
            SetColor(c);
        }

        public void SetColor(Color c) {
            image.color = new Color(c.r, c.g, c.b, 1);
        }

        public void Click() {
            paletteMenu.SelectButton(this);
        }

    }
}
