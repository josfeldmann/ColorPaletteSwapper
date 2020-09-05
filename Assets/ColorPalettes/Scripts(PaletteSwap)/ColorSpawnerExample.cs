using PaletteSwapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PaletteSwapping.Examples {


    public class ColorSpawnerExample : MonoBehaviour {

        public List<ColorPaletteGroup> groups;
        public ColorSwapSprite demoSprite;
        public float spacing = 2;
        private void Awake() {
            MakeExamples();
        }


        public void MakeExamples() {


            for (int y =0; y < groups[0].options.Count; y++) {

                for (int x  = 0; x < groups[1].options.Count; x++) {


                    for (int b = 0; b < groups[2].options.Count; b++) {

                        ColorSwapSprite s = Instantiate(demoSprite, new Vector3((x + b * groups[1].options.Count) * spacing, y * spacing, 0), Quaternion.identity);
                        List<PalettePair> p =  new List<PalettePair>();

                        p.Add(new PalettePair(groups[0].basePalette, groups[0].options[y]));
                        p.Add(new PalettePair(groups[1].basePalette, groups[1].options[x]));
                        p.Add(new PalettePair(groups[2].basePalette, groups[2].options[b]));

                        s.ApplyPalettes(p);
                        s.GetComponent<SpriteRenderer>().sortingOrder = groups[0].options.Count - y;

                    }



                }


            }

        }
        


    }

}
