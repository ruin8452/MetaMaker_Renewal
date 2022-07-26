using Ookii.Dialogs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageContentMenu : MonoBehaviour
{
    [Header("ÀÌ¹ÌÁö ÄÁÅÙÃ÷ ¿µ¿ª Æ®·£½ºÆû")]
    public Transform ImageAssetContentTransform;

    [Header("ÄÁÅÙÃ÷ ¾ÆÀÌÅÛ ÇÁ¸®ÆÕ")]
    public GameObject ContentItemPrefab;

    VistaOpenFileDialog openFileDialog;

    private void Awake()
    {
        openFileDialog = new VistaOpenFileDialog
        {
            Title = "Selecte Image Files",
            Filter = "Image(.png, .jpg, .jpeg)|*.png;*.jpg;*.jpeg",
            Multiselect = true
        };
    }

    private void Start()
    {
        var imageAssets = AssetsContainerManager.GetInstance().GetImageAssets();
        var paths = imageAssets.Select(x => Path.Combine(PathStorage.ASSETS_FOLDER, x.ImageFileName)).ToArray();

        foreach (var filePath in paths)
        {
            ImportImageContent(filePath);
        }
    }

    public void OnClick_AddContentAssets()
    {
        var result = openFileDialog.ShowDialog();

        if (DialogResult.OK != result)
            return;

        foreach(var filePath in openFileDialog.FileNames)
        {
            string destPath = Path.Combine(PathStorage.ASSETS_FOLDER, Path.GetFileName(filePath));

            if (File.Exists(destPath))
                continue;

            File.Copy(filePath, destPath, true);
            ImportImageContent(filePath);
        }
    }

    void ImportImageContent(string imageFilePath)
    {
        var newImageContent = Instantiate(ContentItemPrefab, ImageAssetContentTransform).GetComponent<ContentMenuItem>();

        newImageContent.Icon = SpriteStorage.ReadSprite(imageFilePath);
        newImageContent.UnitName = Path.GetFileName(imageFilePath);
        newImageContent.UnitClass = UnitClasses.ImageUnit;
    }
}
