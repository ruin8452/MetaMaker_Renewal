using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOptionPanel : MonoBehaviour
{
    public ComponentBaseInfo BaseInfo;
    public ComponentTransform Transform;

    List<IComponent> Components;

    private void Awake()
    {
        Components = new List<IComponent>
        {
            BaseInfo,
            Transform
        };
    }


    public void ChangeComponents(string objectClass)
    {
        InvisibleAllComponent();

        BaseInfo.gameObject.SetActive(true);
        Transform.gameObject.SetActive(true);

        switch (objectClass)
        {
            case "Model":
                break;
            case "Image":
                break;
            case "Text":
                break;
            case "Graph":
                break;
            case "CheckBox":
                break;
            case "Document":
                break;
            case "Video":
                break;
        }
    }

    void InvisibleAllComponent()
    {
        Components.ForEach(x => x.SetActive(false));
    }
}
