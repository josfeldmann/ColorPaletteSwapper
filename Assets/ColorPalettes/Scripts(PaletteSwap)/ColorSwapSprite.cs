using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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

    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSwapSprite : MonoBehaviour {


        public SpriteRenderer spriteRenderer;

        public Texture2D colorSwapTexture;
        byte swapTextureIndex;

        public List<PalettePair> palettes;

        public const int maxIndices = 255;

        private static List<PaletteGroup> paletteInfo = new List<PaletteGroup>();
        private static List<int> availableIndexes = null;
        private static int currentIndexCount = 0;
        private static bool textureCleared;

        private Color32 color = new Color32(0, 0, 0, 255);

        private bool initialSetup;

        private void Awake() {



            if (!textureCleared) ClearTexture();


            if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
            ApplyPalettes(palettes);
        }


        public void ClearTexture() {
            for (int x = 0; x < colorSwapTexture.width; x++)
                for (int y = 0; y < colorSwapTexture.height; y++)
                    colorSwapTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));

            textureCleared = true;

        }






        Color32 clear = new Color32(0, 0, 0, 0);


        public void MakeIndices() {
            availableIndexes = new List<int>();

            for (int i = 0; i < maxIndices; i++) {
                availableIndexes.Add(i);
            }

        }

        public void ApplyPalettes(List<PalettePair> p1) {

            if (availableIndexes == null) MakeIndices();

            RemoveCurrentPalette();
            palettes = p1;
            SetSwapTexture();
            List<ColorPalette> key = new List<ColorPalette>();
            foreach (PalettePair p in palettes) {
                key.Add(p.swapPalette);
            }

            int index = -1;

            foreach (PaletteGroup p in paletteInfo) {

                if (p.colors.Count == key.Count) {
                    if (ListHelper.ContainsAll(p.colors, key)) {

                        if (!p.currentUnitsWithPalette.Contains(this)) {
                            p.currentUnitsWithPalette.Add(this);
                        }
                        index = p.id;
                        break;

                    }
                }
            }

            if (index == -1) {

                int newID = availableIndexes[0];
                availableIndexes.RemoveAt(0);
                // print("AddingNewThing");

                PaletteGroup p = new PaletteGroup(newID, key);
                p.currentUnitsWithPalette.Add(this);
                paletteInfo.Add(p);
                index = newID;
            }




            swapTextureIndex = (byte)index;




            foreach (PalettePair p in palettes) {
                SetColors(p.basePalette, p.swapPalette);
            }


            colorSwapTexture.Apply();


            color = spriteRenderer.color;
            color.a = swapTextureIndex;
            spriteRenderer.color = color;





        }


        void SetColors(ColorPalette bp, ColorPalette cp) {





            if (bp.colors.Length != cp.colors.Length) {
                Debug.LogError("Palettes are not the same size!");
                return;
            }


            for (int x = 0; x < cp.colors.Length; x++) {
                //  print(x + ": " + bp.colors[x].r);
                colorSwapTexture.SetPixel(bp.colors[x].r, swapTextureIndex, cp.colors[x]);
            }





        }

        void SetSwapTexture() {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(properties);
            properties.SetTexture("_SwapTex", colorSwapTexture);
            spriteRenderer.SetPropertyBlock(properties);
            //setup = true;
        }

        private void OnDestroy() {

            RemoveCurrentPalette();

        }


        public void RemoveCurrentPalette() {


            if (!initialSetup) {
                initialSetup = true;
                return;
            }

            List<ColorPalette> key = new List<ColorPalette>();

            foreach (PalettePair p in palettes) {
                key.Add(p.swapPalette);
            }

            foreach (PaletteGroup p in paletteInfo) {
                if (p.colors.Count == key.Count) {
                    if (ListHelper.ContainsAll(p.colors, key)) {

                        p.currentUnitsWithPalette.Remove(this);
                        if (p.currentUnitsWithPalette.Count <= 0) {
                            int pIndex = p.id;
                           // print("removed sucessfully " + Time.time);
                            availableIndexes.Add(pIndex);
                            paletteInfo.Remove(p);
                            break;
                        }

                    }
                }
            }


        }


    }





    public class ListHelper {

        public static bool ContainsAll<T>(IEnumerable<T> source, IEnumerable<T> values) {
            return values.All(value => source.Contains(value));
        }


    }

}




