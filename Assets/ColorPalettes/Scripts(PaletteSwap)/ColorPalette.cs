using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PaletteSwapping {

    [CreateAssetMenu(fileName = "New Palette", menuName = "Palettes/ColorPalette")]
    public class ColorPalette : ScriptableObject {
        public string paletteName;
        public Color32[] colors;
    }


}