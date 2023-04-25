using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDeselect : MonoBehaviour
{
    GameObject BlockerPanel;

    private void Awake()
    {
        BlockerPanel = GameObject.FindGameObjectWithTag(Cache.TAG_BLOCKER).transform.GetChild(0).gameObject;
        BlockerPanel.GetComponent<Button>().onClick.AddListener(HideView);
    }

    private void OnEnable()
    {
        BlockerPanel.SetActive(true);
    }

    void HideView()
    {
        gameObject.SetActive(false);
        BlockerPanel.SetActive(false);
    }
}
