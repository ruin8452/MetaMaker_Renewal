using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorSystem : MonoBehaviour
{
    public Texture2D NormalCursor;
    public Texture2D SideCursor;

    Vector2 cursorHotspot;
    bool isDragging;

    private void Awake()
    {
        cursorHotspot = new Vector2(SideCursor.width / 2, SideCursor.height / 2);
    }

    private void Start()
    {
        SetCursor("Normal");
    }

    public void SetCursor(string cursorType)
    {
        if (isDragging) return;

        switch (cursorType)
        {
            case "Normal":
                Cursor.SetCursor(NormalCursor, Vector2.zero, CursorMode.ForceSoftware);
                break;
            case "Side":
                Cursor.SetCursor(SideCursor, cursorHotspot, CursorMode.ForceSoftware);
                break;
        }
    }

    public void BegineDrag(string cursorType)
    {
        SetCursor(cursorType);
        isDragging = true;
    }

    public void EndDrag(string cursorType)
    {
        isDragging = false;
        SetCursor(cursorType);
    }
}
