using System.Collections.Generic;


namespace PaletteSwapping {
    public class PaletteGroup {

        public int id;
        public List<ColorSwapSprite> currentUnitsWithPalette;
        public List<ColorPalette> colors;

        public PaletteGroup(int id, List<ColorPalette> colors) {
            this.id = id;
            currentUnitsWithPalette = new List<ColorSwapSprite>();
            this.colors = colors;
        }
    }

}




