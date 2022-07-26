using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_PrintLog : MonoBehaviour
{
    public static DEBUG_Display Display;

    private void Awake()
    {
        Display = GetComponent<DEBUG_Display>();
    }
    public static void PrintLog(string p_str)
    {
#if DEBUG
        Debug.Log(p_str);

        if(Display)
        {
            Display.TypingLog( p_str);
        }
#else


#endif
    }



}
