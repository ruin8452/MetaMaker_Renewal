using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneManager_Home : MonoBehaviour
{
    private void Awake()
    {
        PathStorage.CheckDefineDirectory();
    }

    public void OnClick_Exit()
    {
        Application.Quit();
#if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButton(0))
    //    {
    //        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    //    }
    //}
}
