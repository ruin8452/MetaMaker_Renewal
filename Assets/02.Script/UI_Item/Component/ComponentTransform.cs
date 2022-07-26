using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentTransform : MonoBehaviour, IComponent
{
    [Header("< Position >")]
    public TMP_InputField InputPosX;
    public TMP_InputField InputPosY;
    public TMP_InputField InputPosZ;

    [Header("< Rotation >")]
    public TMP_InputField InputRotX;
    public TMP_InputField InputRotY;
    public TMP_InputField InputRotZ;

    [Header("< Scale >")]
    public TMP_InputField InputScaX;
    public TMP_InputField InputScaY;
    public TMP_InputField InputScaZ;

    public bool isLocal;
    public bool isScaleSync;

    public Action<string, float> ChangeTanstformPos;
    public Action<string, float> ChangeTanstformRot;
    public Action<string, float> ChangeTanstformSca;

    public void OnClick_Copy()
    {

    }

    public void OnClick_Paste()
    {

    }

    public void OnClick_Reset()
    {
        InputPosX.text = "0";
        InputPosY.text = "0";
        InputPosZ.text = "0";

        InputRotX.text = "0";
        InputRotY.text = "0";
        InputRotZ.text = "0";

        InputScaX.text = "1";
        InputScaY.text = "1";
        InputScaZ.text = "1";
    }

    public void OnToggle_TransformType(bool isLocal)
    {
    }

    public void OnToggle_ScaleSync(bool isScaleSync)
    {
        this.isScaleSync = isScaleSync;

        InputScaX.onValueChanged.RemoveAllListeners();
        InputScaY.onValueChanged.RemoveAllListeners();
        InputScaZ.onValueChanged.RemoveAllListeners();

        if (isScaleSync)
        {
            InputScaX.onValueChanged.AddListener(OnTextChange_SyncScale);
            InputScaY.onValueChanged.AddListener(OnTextChange_SyncScale);
            InputScaZ.onValueChanged.AddListener(OnTextChange_SyncScale);
        }
        else
        {
            InputScaX.onValueChanged.AddListener(OnTextChange_ScaX);
            InputScaY.onValueChanged.AddListener(OnTextChange_ScaY);
            InputScaZ.onValueChanged.AddListener(OnTextChange_ScaZ);
        }
    }





    public void OnTextChange_PosX(string posX) { if (float.TryParse(posX, out float numPosX)) ChangeTanstformPos?.Invoke("X", numPosX); }
    public void OnTextChange_PosY(string posY) { if (float.TryParse(posY, out float numPosY)) ChangeTanstformPos?.Invoke("Y", numPosY); }
    public void OnTextChange_PosZ(string posZ) { if (float.TryParse(posZ, out float numPosZ)) ChangeTanstformPos?.Invoke("Z", numPosZ); }

    public void OnTextChange_RotX(string rotX) { if (float.TryParse(rotX, out float numRotX)) ChangeTanstformRot?.Invoke("X", numRotX); }
    public void OnTextChange_RotY(string rotY) { if (float.TryParse(rotY, out float numRotY)) ChangeTanstformRot?.Invoke("Y", numRotY); }
    public void OnTextChange_RotZ(string rotZ) { if (float.TryParse(rotZ, out float numRotZ)) ChangeTanstformRot?.Invoke("Z", numRotZ); }

    public void OnTextChange_ScaX(string scaX) { if (float.TryParse(scaX, out float numScaX)) ChangeTanstformSca?.Invoke("X", numScaX); }
    public void OnTextChange_ScaY(string scaY) { if (float.TryParse(scaY, out float numScaY)) ChangeTanstformSca?.Invoke("Y", numScaY); }
    public void OnTextChange_ScaZ(string scaZ) { if (float.TryParse(scaZ, out float numScaZ)) ChangeTanstformSca?.Invoke("Z", numScaZ); }


    public void OnEndEdit_EmptyPosX(string text) => InputPosX.text = float.TryParse(text, out _) ? text : "0";
    public void OnEndEdit_EmptyPosY(string text) => InputPosY.text = float.TryParse(text, out _) ? text : "0";
    public void OnEndEdit_EmptyPosZ(string text) => InputPosZ.text = float.TryParse(text, out _) ? text : "0";

    public void OnEndEdit_EmptyRotX(string text) => InputRotX.text = float.TryParse(text, out _) ? text : "0";
    public void OnEndEdit_EmptyRotY(string text) => InputRotY.text = float.TryParse(text, out _) ? text : "0";
    public void OnEndEdit_EmptyRotZ(string text) => InputRotZ.text = float.TryParse(text, out _) ? text : "0";

    public void OnEndEdit_EmptyScaX(string text) => InputScaX.text = float.TryParse(text, out _) ? text : "1";
    public void OnEndEdit_EmptyScaY(string text) => InputScaY.text = float.TryParse(text, out _) ? text : "1";
    public void OnEndEdit_EmptyScaZ(string text) => InputScaZ.text = float.TryParse(text, out _) ? text : "1";

    public void OnTextChange_SyncScale(string Scale)
    {
        InputScaX.text = Scale;
        InputScaY.text = Scale;
        InputScaZ.text = Scale;
    }

    public void SetTransformComponent(Vector3 pos, Vector3 rot, Vector3 sca)
    {
        InputPosX.text = pos.x.ToString();
        InputPosY.text = pos.y.ToString();
        InputPosZ.text = pos.z.ToString();

        InputRotX.text = rot.x.ToString();
        InputRotY.text = rot.y.ToString();
        InputRotZ.text = rot.z.ToString();

        InputScaX.text = sca.x.ToString();
        InputScaY.text = sca.y.ToString();
        InputScaZ.text = sca.z.ToString();
    }


    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
