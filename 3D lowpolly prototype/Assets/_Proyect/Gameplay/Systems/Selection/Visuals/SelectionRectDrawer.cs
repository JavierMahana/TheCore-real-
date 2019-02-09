using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRectDrawer : MonoBehaviour
{
    private bool isSelecting = false;
    private Vector2 firstTouchPosition;
    private Vector2 actualTouchPosition;

    public void RectSelection(Vector2 screenPos1, Vector2 screenPos2)
    {
        isSelecting = true;
        firstTouchPosition = screenPos1;
        actualTouchPosition = screenPos2;
    }
    private void OnGUI()
    {
        if (isSelecting)
        {
            Rect rect = UtilityGUI.GetScreenRect(firstTouchPosition, actualTouchPosition);
            UtilityGUI.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            UtilityGUI.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
