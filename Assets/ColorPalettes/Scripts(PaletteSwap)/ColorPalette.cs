using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PaletteSwapping {

    [CreateAssetMenu(fileName = "New Palette", menuName = "Palettes/ColorPalette")]
    public class ColorPalette : ScriptableObject {
        public string paletteName;
        public Color32[] colors;

        public ColorPalette(Color32[] col) {
            this.colors = new Color32[col.Length];
            for (int i = 0; i < col.Length; i++) {
                this.colors[i] = new Color32(col[i].r, col[i].g, col[i].b, 1);
            }
        }
    }


}