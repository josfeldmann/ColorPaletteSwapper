using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PaletteSwapping {



    public class CustomPaletteCreator : MonoBehaviour {

        public List<ColorPaletteGroup> groups;
        public ColorSwapSprite demoSprite;
        public CustomPaletteSelectButton customPaletteSelectPrefab;
        private List<CustomPaletteSelectButton> buttons = new List<CustomPaletteSelectButton>();
        public Transform sortingSpot;
        public PaletteColorMenu colorMenu;


        private void Awake() {
            MakePanels();
            colorMenu.Close();
            colorMenu.gameObject.SetActive(false);
        }

        public void EditPalette(CustomPaletteSelectButton c) {
            colorMenu.gameObject.SetActive(true);
            colorMenu.init(c.palette);
            gameObject.SetActive(false);
        }

        public void ReturnToPaletteSelect() {

            colorMenu.Close();
            colorMenu.gameObject.SetActive(false);
            gameObject.SetActive(true);
            
        }


        public void MakePanels() {

            foreach (ColorPaletteGroup g in groups) {

                CustomPaletteSelectButton c = Instantiate(customPaletteSelectPrefab, sortingSpot);
                c.init(g.basePalette, new ColorPalette(g.basePalette.colors), this, g.name);
                buttons.Add(c);
            }
            UpdateDemoBasedOnPanels();

        }

        public void UpdateDemoBasedOnPanels() {

            List<PalettePair> pairs = new List<PalettePair>();
            
            foreach (CustomPaletteSelectButton c in buttons) {
                PalettePair p = new PalettePair(c.basePalette, c.palette);
                pairs.Add(p);
            }

            demoSprite.ApplyPalettes(pairs);



        }
    }

}
