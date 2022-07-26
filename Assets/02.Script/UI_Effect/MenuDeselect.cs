using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDeselect : MonoBehaviour
{
    public GameObject BlockerPanel;

    private void Awake()
    {
        BlockerPanel.GetComponent<Button>().onClick.AddListener(HideView);
    }

    void HideView()
    {
        gameObject.SetActive(false);
        BlockerPanel.SetActive(false);
    }
}
