using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Home : MonoBehaviour
{
    private void Awake()
    {
        PathStorage.CheckDefineDirectory();
    }
}
