# Color Palette Swapper

A Unity library that lets you swap individual sprites color palettes at runtime dynamically. Uses a color swapping shader and a render texture to change the colors a sprite renders.



## How To Use

1. Add the ColorPaletteSwapper unitypackage to your unity project
2. Add a ColorSwapSprite component to a prefab/gameobject with a SpriteRenderer component attached
3. Right click in the project window and go to Create => Palettes => ColorPalette. This will create a ScriptableObject that we can use to indicate which colors we want to change.
4. Add all the colors you want to swap in the target sprite into the Colors array of the new palette object.
5. Use Create => Palettes => ColorPalette to create a target palette that will contain all of the colors you want to swap the orignal sprite colors with

