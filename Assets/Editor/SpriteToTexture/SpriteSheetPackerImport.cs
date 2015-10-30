
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

/// <summary>
/// 将图集中的图片提取出来
/// </summary>
public class SpriteSheetPackerImport
{
    [MenuItem("Tools/Sprite Sheet Packer/Process to Sprites")]
    static void ProcessToSprite()
    {
        Texture2D image = Selection.activeObject as Texture2D;//获取选择的对象
        Texture2D imageCopy = SpriteSheetSelect.imageCopy;

        //获取路径名称，以及图片路径
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));
        string path = rootPath + "/" + image.name + ".PNG";

        //修改导入设置从导入的图片中
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;

        //在同样的路径下创建文件夹
        AssetDatabase.CreateFolder(rootPath, image.name);

        //遍历小图集
        foreach (SpriteMetaData metaData in texImp.spritesheet)
        {
            Texture2D myImage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

            //abc_0:(x:2.00, y:400.00, width:103.00, height:112.00)
            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)
            {
                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                {
                    myImage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, imageCopy.GetPixel(x, y));
                }
            }

            //转换纹理到PNG兼容格式
            if (myImage.format != TextureFormat.ARGB32 && myImage.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(myImage.width, myImage.height);
                newTexture.SetPixels(myImage.GetPixels(0), 0);
                myImage = newTexture;
            }

            var pngData = myImage.EncodeToPNG();

            File.WriteAllBytes(rootPath + "/" + image.name + "/" + metaData.name + ".PNG", pngData);
        }
    }
}

