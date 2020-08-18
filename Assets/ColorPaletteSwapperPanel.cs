using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace PaletteSwapping {
    public class ColorPaletteSwapperPanel : MonoBehaviour {
        private ColorPaletteGroup paletteGroup;
        private CharacterColorCustomizer customizer;
        public int currentIndex = 0;

        [SerializeField]
        private TextMeshProUGUI paletteText, categoryText;


        public void init(ColorPaletteGroup group, CharacterColorCustomizer t) {

            customizer = t;
            paletteGroup = group;
            paletteText.text = group.options[currentIndex].paletteName;
            categoryText.text = group.name;

        }

        public void MoveIndexInDirection(int direction) {

            currentIndex += direction;

            if (currentIndex < 0) {
                currentIndex = paletteGroup.options.Count - 1;
            } else if (currentIndex >= paletteGroup.options.Count) {
                currentIndex = 0;
            }

            paletteText.text = paletteGroup.options[currentIndex].paletteName;

            customizer.UpdateDemoBasedOnPanels();

        }
   }

}
