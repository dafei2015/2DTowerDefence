using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpriteSheetSelect
{
    public static Texture2D imageCopy;

    [MenuItem("Tools/Sprite Sheet Packer/Select to Sprites")]
    static void SpriteImageSelect()
    {
        imageCopy = Selection.activeObject as Texture2D;
    }
}
