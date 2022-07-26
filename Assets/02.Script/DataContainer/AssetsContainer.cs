using Assets._02.Script.DataContainer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AssetsContainer : IJsonContainer
{
    public List<ModelAsset> ModelAssets;
    public List<ImageAsset> ImageAssets;
    public List<DocumentAsset> DocumentAssets;
    public List<VideoAsset> VideoAssets;

    [Serializable]
    public class ModelAsset
    {
        public string ModelFileName;
        public string IconFileName;
        public string Version;

        public ModelAsset(string modelFileName, string iconFileName)
        {
            ModelFileName = modelFileName;
            IconFileName = iconFileName;
            Version = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        public override bool Equals(object obj)
        {
            return ModelFileName == ((ModelAsset)obj).ModelFileName;
        }
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(ModelFileName) ? 0 : ModelFileName.GetHashCode();
        }
    }

    [Serializable]
    public class ImageAsset
    {
        public string ImageFileName;
        public string Version;

        public ImageAsset(string imageFileName)
        {
            ImageFileName = imageFileName;
            Version = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        public override bool Equals(object obj)
        {
            return ImageFileName == ((ImageAsset)obj).ImageFileName;
        }
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(ImageFileName) ? 0 : ImageFileName.GetHashCode();
        }
    }

    [Serializable]
    public class DocumentAsset
    {
        public string DocFileName;
        public string IconFileName;
        public string Version;

        public DocumentAsset(string docFileName, string iconFileName)
        {
            DocFileName = docFileName;
            IconFileName = iconFileName;
            Version = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        public override bool Equals(object obj)
        {
            return DocFileName == ((DocumentAsset)obj).DocFileName;
        }
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(DocFileName) ? 0 : DocFileName.GetHashCode();
        }
    }

    [Serializable]
    public class VideoAsset
    {
        public string VideoFileName;
        public string IconFileName;
        public string Version;

        public VideoAsset(string videoFileName, string iconFileName)
        {
            VideoFileName = videoFileName;
            IconFileName = iconFileName;
            Version = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        public override bool Equals(object obj)
        {
            return VideoFileName == ((VideoAsset)obj).VideoFileName;
        }
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(VideoFileName) ? 0 : VideoFileName.GetHashCode();
        }
    }

    public void InitContainer()
    {
        ModelAssets = new List<ModelAsset>();
        ImageAssets = new List<ImageAsset>();
        DocumentAssets = new List <DocumentAsset>();
        VideoAssets = new List <VideoAsset>();
}
}

public class AssetsContainerManager
{
    AssetsContainer assetsContainer;
    static AssetsContainerManager containerManager;

    AssetsContainerManager()
    {
        assetsContainer = JsonStorage.ReadJson<AssetsContainer>(PathStorage.ASSETS_LISTUP);
    }

    public static AssetsContainerManager GetInstance()
    {
        if (containerManager == null)
            containerManager = new AssetsContainerManager();
        return containerManager;
    }

    void SaveContainer()
    {
        JsonStorage.WriteJson(assetsContainer, PathStorage.ASSETS_LISTUP);
    }

    public void AddModelAsset(string modelFileName, string iconFileName)
    {
        assetsContainer.ModelAssets.Add(new AssetsContainer.ModelAsset(modelFileName, iconFileName));
        assetsContainer.ModelAssets = assetsContainer.ModelAssets.Distinct().ToList();
        SaveContainer();
    }

    public void AddImageAsset(string imageFileName)
    {
        assetsContainer.ImageAssets.Add(new AssetsContainer.ImageAsset(imageFileName));
        assetsContainer.ImageAssets = assetsContainer.ImageAssets.Distinct().ToList();
        SaveContainer();
    }

    public void AddDocumentAsset(string docFileName, string iconFileName)
    {
        assetsContainer.DocumentAssets.Add(new AssetsContainer.DocumentAsset(docFileName, iconFileName));
        assetsContainer.DocumentAssets = assetsContainer.DocumentAssets.Distinct().ToList();
        SaveContainer();
    }

    public void AddVideoAsset(string videoFileName, string iconFileName)
    {
        assetsContainer.VideoAssets.Add(new AssetsContainer.VideoAsset(videoFileName, iconFileName));
        assetsContainer.VideoAssets = assetsContainer.VideoAssets.Distinct().ToList();
        SaveContainer();
    }



    public void DeleteModelAsset(string modelFileName)
    {
        assetsContainer.ModelAssets.RemoveAll(x => x.ModelFileName == modelFileName);
        SaveContainer();
    }

    public void DeleteImageAsset(string imageFileName)
    {
        assetsContainer.ImageAssets.RemoveAll(x => x.ImageFileName == imageFileName);
        SaveContainer();
    }

    public void DeleteDocumentAsset(string docFileName)
    {
        assetsContainer.DocumentAssets.RemoveAll(x => x.DocFileName == docFileName);
        SaveContainer();
    }

    public void DeleteVideoAsset(string videoFileName)
    {
        assetsContainer.VideoAssets.RemoveAll(x => x.VideoFileName == videoFileName);
        SaveContainer();
    }



    public List<AssetsContainer.ModelAsset> GetModelAssets()
    {
        return assetsContainer.ModelAssets;
    }

    public List<AssetsContainer.ImageAsset> GetImageAssets()
    {
        return assetsContainer.ImageAssets;
    }

    public List<AssetsContainer.DocumentAsset> GetDocumentAssets()
    {
        return assetsContainer.DocumentAssets;
    }

    public List<AssetsContainer.VideoAsset> GetVideoAssets()
    {
        return assetsContainer.VideoAssets;
    }
}
