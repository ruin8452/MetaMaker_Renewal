using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ookii.Dialogs;
using System.Windows.Forms;
using TriLibCore;
using System.IO;

public class ModelContentMenu : MonoBehaviour
{
    public Transform ModelAssetContentTransform;
    public GameObject ContentItemPrefab;
    public AssetLoaderOptions AssetLoaderOption;
    public Camera CaptureCamera;

    VistaOpenFileDialog openFileDialog;

    private void Awake()
    {
        openFileDialog = new VistaOpenFileDialog
        {
            Title = "Selecte 3D Model Files",
            Filter = "3D Model(.fbx, .obj);*.fbx,*.obj",
            Multiselect = true
        };
    }

    public void OnClick_AddContentAssets()
    {
        var result = openFileDialog.ShowDialog();

        if (DialogResult.OK == result)
        {
            StartCoroutine(LoadModelFileAtRunTime(openFileDialog.FileNames));
        }
    }

    IEnumerator LoadModelFileAtRunTime(string[] modelFilePaths)
    {
        foreach (var modelFilePath in modelFilePaths)
        {
            GameObject newModelObject = Instantiate(ContentItemPrefab, ModelAssetContentTransform);
            AssetLoader.LoadModelFromFileNoThread(modelFilePath, wrapperGameObject: newModelObject, assetLoaderOptions: AssetLoaderOption);

            yield return Cache.CachingWaitForSecond(0.01f);

            Sprite newModelIcon = SpriteStorage.TakeTexture(CaptureCamera.targetTexture);
            string spriteFilePath = PathStorage.ChangePath(modelFilePath, "", Path.GetFileNameWithoutExtension(modelFilePath), ".png");
            SpriteStorage.SaveSprite(newModelIcon, spriteFilePath);
        }
    }
}
