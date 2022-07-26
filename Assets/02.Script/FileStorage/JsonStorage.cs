using Assets._02.Script.DataContainer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonStorage : MonoBehaviour
{
    public static bool WriteJson<T>(T container, string fullPath) where T : IJsonContainer
    {
        string jsonText = JsonUtility.ToJson(container, true);

        File.WriteAllText(fullPath, jsonText);

        return File.Exists(fullPath);
    }

    public static T ReadJson<T>(string fullPath) where T : IJsonContainer
    {
        if (!File.Exists(fullPath))
            return default;

        string jsonText = File.ReadAllText(fullPath);
        T jsonContainer = JsonUtility.FromJson<T>(jsonText);

        return jsonContainer;
    }
}
