using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System;

public class ControlPanel : MonoBehaviour, IBasePanel
{
    [SerializeField]
    private SelectionManager selectionManager;

    private enum GestureType { TAP, DOUBLE_TAP, DRAG }
    // este panel tiene gestures solo en el escenarío. En el panel habran botones simples y el panel en sí bloquea las gestures.
    private TapGestureRecognizer tapGesture;
    private TapGestureRecognizer doubleTapGesture;
    private PanGestureRecognizer dragGesture;

    private void Start()
    {
        CreateGestures();

    }
    private void CreateGestures()
    {
        CreateTapGesture();
        CreateDoubleTapGesture();
        CreateDragGesture();
    }

    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapCallback;
    }
    private void TapCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {

        }
        if (gesture.State == GestureRecognizerState.Ended)
        {

        }
    }

    private void CreateDoubleTapGesture()
    {
        doubleTapGesture = new TapGestureRecognizer();
        doubleTapGesture.NumberOfTapsRequired = 2;
        doubleTapGesture.StateUpdated += DoubleTapCallback;
    }
    private void DoubleTapCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            
        }
        if (gesture.State == GestureRecognizerState.Ended)
        {

        }
    }
    private void CreateDragGesture()
    {
        dragGesture = new PanGestureRecognizer();
        dragGesture.StateUpdated += DragCallback;
    }
    private void DragCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {

        }
        if (gesture.State == GestureRecognizerState.Ended)
        {
            selectionManager.EndRectSelection(new Vector2(gesture.StartFocusX, gesture.StartFocusY), new Vector2(gesture.FocusX, gesture.FocusY));
        }
    }
    public void InitializePanel()
    {
        FingersScript.Instance.AddGesture(tapGesture);
        FingersScript.Instance.AddGesture(doubleTapGesture);
        FingersScript.Instance.AddGesture(dragGesture);
    }
    public void DeactivatePanel()
    {
        FingersScript.Instance.RemoveGesture(tapGesture);
        FingersScript.Instance.RemoveGesture(doubleTapGesture);
        FingersScript.Instance.RemoveGesture(dragGesture);
    }

}
