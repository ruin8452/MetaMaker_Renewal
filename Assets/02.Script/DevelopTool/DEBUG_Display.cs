using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUG_Display : MonoBehaviour
{
    public TextMeshProUGUI TMP_Display;


    void Awake()
    {
#if !DEV
        Destroy(gameObject);

#endif
        TMP_Display.text = "";
    }

    public void TypingLog(string p_str)
    {
        if (TMP_Display)
            TMP_Display.text += "\n" + p_str;
    }

}
