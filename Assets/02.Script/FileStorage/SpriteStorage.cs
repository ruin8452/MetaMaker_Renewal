using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteStorage
{
    public static Sprite ReadSprite(string fullPath)
    {
        Texture2D selectTexture = new Texture2D(0, 0);
        selectTexture.LoadImage(File.ReadAllBytes(fullPath));

        return Sprite.Create(selectTexture, new Rect(0, 0, selectTexture.width, selectTexture.height), new Vector2(0.5f, 0.5f));
    }

    public static bool SaveSprite(Sprite sprite, string fullPath)
    {
        File.WriteAllBytes(fullPath, sprite.texture.EncodeToPNG());

        return File.Exists(fullPath);
    }

    public static Sprite TakeTexture(RenderTexture texture)
    {
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = texture;
        tex.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        tex.Apply();

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }
}
