using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtilities 
{
    private static float viewPortSeparationBetweenPanelAndScene = 0;

    public static float GetTopCoordinateOfRectTransformInViewPortSpace(RectTransform rectTransform, Canvas canvas)
    {
        if (viewPortSeparationBetweenPanelAndScene == 0)
        {
            viewPortSeparationBetweenPanelAndScene = CameraUtility.Instance.MainCamera.ScreenToViewportPoint(new Vector2(0, GetTopCoordinateOfRectTransformInScreenSpace(rectTransform, canvas))).y;
        }
        return viewPortSeparationBetweenPanelAndScene;
    }
    private static float GetTopCoordinateOfRectTransformInScreenSpace(RectTransform rectTransform, Canvas canvas)
    {
        Rect rectTransformRect = RectTransformUtility.PixelAdjustRect(rectTransform, canvas);
        return rectTransformRect.max.y;
    }
}
