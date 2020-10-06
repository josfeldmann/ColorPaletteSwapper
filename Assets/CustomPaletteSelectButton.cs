using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PaletteSwapping {
    public class CustomPaletteSelectButton : MonoBehaviour {

        CustomPaletteCreator creator;
        public ColorPalette basePalette;
        public ColorPalette palette;
        [SerializeField] private TextMeshProUGUI text;

        public void init(ColorPalette bPalette, ColorPalette c, CustomPaletteCreator p, string palleteGroupName) {
            creator = p;
            palette = c;
            basePalette = bPalette;
            text.text = palleteGroupName;
        }

        public void Click() {
            creator.EditPalette(this);
        }


    }
}