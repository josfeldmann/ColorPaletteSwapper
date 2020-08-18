using UnityEngine;



namespace PaletteSwapping {
    [System.Serializable]
    public class PalettePair {


        [HideInInspector]
        public string name = "Palette";
        public ColorPalette basePalette;
        public ColorPalette swapPalette;

        public PalettePair(ColorPalette basePalette, ColorPalette swapPalette) {
            this.basePalette = basePalette;
            this.swapPalette = swapPalette;
        }
    }

}
