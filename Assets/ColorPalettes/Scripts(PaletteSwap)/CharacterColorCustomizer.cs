using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PaletteSwapping {

    [System.Serializable]
    public class ColorPaletteGroup {

        public string name;
        public ColorPalette basePalette;
        public List<ColorPalette> options;


    }



    public class CharacterColorCustomizer : MonoBehaviour {

        public List<ColorPaletteGroup> groups;
        public ColorSwapSprite demoSprite;
        public ColorPaletteSwapperPanel panelPrefab;
        private List<ColorPaletteSwapperPanel> panels = new List<ColorPaletteSwapperPanel>();
        public Transform sortingSpot;


        private void Awake() {
            MakePanels();
        }


        public void MakePanels() {

            foreach (ColorPaletteGroup g in groups) {

                ColorPaletteSwapperPanel c = Instantiate(panelPrefab, sortingSpot);
                c.init(g, this);
                panels.Add(c);


            }
            UpdateDemoBasedOnPanels();

        }

        internal void UpdateDemoBasedOnPanels() {

            List<PalettePair> pairs = new List<PalettePair>();


            for (int i = 0; i < panels.Count; i++) {

                PalettePair p = new PalettePair(groups[i].basePalette, groups[i].options[panels[i].currentIndex]);
                pairs.Add(p);

            }

            demoSprite.ApplyPalettes(pairs);



        }
    }

}
