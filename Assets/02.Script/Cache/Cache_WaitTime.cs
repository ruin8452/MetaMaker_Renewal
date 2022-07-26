using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Cache : MonoBehaviour
{
    public static WaitForSeconds Time_60fps = new WaitForSeconds(0.016f);

    static Dictionary<float, WaitForSeconds> WaitTimeCache = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds CachingWaitForSecond(float p_time)
    {
        if (!WaitTimeCache.ContainsKey(p_time))
        {
            WaitTimeCache.Add(p_time, new WaitForSeconds(p_time));
        }

        return WaitTimeCache[p_time];
    }
}
