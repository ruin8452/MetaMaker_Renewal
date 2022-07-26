using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathStorage
{
    public static string ASSETS_FOLDER = Path.Combine(Application.persistentDataPath, "Assests");
    public static string ASSETS_LISTUP = Path.Combine(Application.persistentDataPath, "Assests.json");

    public static void CheckDefineDirectory()
    {
        DEBUG_PrintLog.PrintLog("<< Cache.ROOT_PATH : " + ASSETS_FOLDER);
        DEBUG_PrintLog.PrintLog("<< Cache.SA_PATH : " + Application.streamingAssetsPath);

        if (!Directory.Exists(ASSETS_FOLDER))
            Directory.CreateDirectory(ASSETS_FOLDER);
    }


    public static string ChangePath(string sourcePath, string changeDir = "", string changeFileName = "", string changeExt = "")
    {
        string tempDir      = string.IsNullOrEmpty(changeDir)      ? Path.GetDirectoryName(sourcePath) : changeDir;
        string tempFileName = string.IsNullOrEmpty(changeFileName) ? Path.GetFileNameWithoutExtension(sourcePath) : changeFileName;
        string tempExt      = string.IsNullOrEmpty(changeExt)      ? Path.GetExtension(sourcePath) : changeExt;

        return Path.Combine(tempDir, $"{tempFileName}{tempExt}");
    }
}
