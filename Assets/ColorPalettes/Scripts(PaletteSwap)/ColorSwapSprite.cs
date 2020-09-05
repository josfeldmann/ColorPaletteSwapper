using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace PaletteSwapping {

    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSwapSprite : MonoBehaviour {

        public const int MAXINDICES = 255;

        public bool swapOnAwake = true;
        public Texture2D colorSwapTexture;
        public List<PalettePair> palettes;

        private SpriteRenderer spriteRenderer;
        private static List<PaletteGroup> paletteInfo = new List<PaletteGroup>();
        private static List<int> availableIndexes = null;
        private static bool textureCleared;
        byte swapTextureIndex;
        private bool initialSetup;
        private Color32 color = new Color32(0, 0, 0, 255);

        private void Awake() {
            if (!textureCleared) ClearTexture();
            if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
            if (swapOnAwake) ApplyPalettes(palettes);
        }

       
        //Call this to perform the swap
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

                PaletteGroup p = new PaletteGroup(newID, key);
                p.currentUnitsWithPalette.Add(this);
                paletteInfo.Add(p);
                index = newID;
            }

            swapTextureIndex = (byte)index;

            //Populate the renderTexture
            foreach (PalettePair p in palettes) {
                SetColors(p.basePalette, p.swapPalette);
            }

            colorSwapTexture.Apply();

            color = spriteRenderer.color;
            color.a = swapTextureIndex;
            spriteRenderer.color = color;
        }

        //Function that actually populates the render texture with the desired color pixels
        private void SetColors(ColorPalette bp, ColorPalette cp) {

            if (bp.colors.Length != cp.colors.Length) {
                Debug.LogError("Palettes are not the same size!");
                return;
            }

            for (int x = 0; x < cp.colors.Length; x++) {
                colorSwapTexture.SetPixel(bp.colors[x].r, swapTextureIndex, cp.colors[x]);
            }
        }

        //Apply the swap texture to the sprite renderer
        private void SetSwapTexture() {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(properties);
            properties.SetTexture("_SwapTex", colorSwapTexture);
            spriteRenderer.SetPropertyBlock(properties);
            //setup = true;
        }

        private void OnDestroy() {
            RemoveCurrentPalette();
        }

        //Removes a count the palette from the database.
        //If no more ColorSwapSprites with that palette combination exist then remove it from the database
        private void RemoveCurrentPalette() {


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
                            availableIndexes.Add(pIndex);
                            paletteInfo.Remove(p);
                            break;
                        }
                    }
                }
            }
        }


        //Clears the swap texture initially
        private void ClearTexture() {
            for (int x = 0; x < colorSwapTexture.width; x++)
                for (int y = 0; y < colorSwapTexture.height; y++)
                    colorSwapTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));

            textureCleared = true;
        }

        //Make indices for color swap database
        private void MakeIndices() {
            availableIndexes = new List<int>();
            for (int i = 0; i < MAXINDICES; i++) {
                availableIndexes.Add(i);
            }
        }


    }





    public class ListHelper {

        public static bool ContainsAll<T>(IEnumerable<T> source, IEnumerable<T> values) {
            return values.All(value => source.Contains(value));
        }


    }

}




