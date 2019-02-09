using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System;

public class InputManager : Singleton<InputManager>
{
    private IBasePanel activePanel;

    private void OnPanelChange(IBasePanel newActivePanel)
    {
        activePanel.DeactivatePanel();

        activePanel = newActivePanel;
        activePanel.InitializePanel();
    }


    #region bullShit

    //public const int MAX_RAY_DISTANCE = 200;
    //[SerializeField]
    //new private Camera camera;
    //[SerializeField]
    //private LayerMask buyingButtonsLayers;
    //[SerializeField]
    //private LayerMask selectableLayers;
    //[SerializeField]
    //private LayerMask movableLayers;
    //[SerializeField]
    //private bool debug;

    //public Action<Vector2, BuyButton> DragButton = delegate { };
    //public Action<Vector2, BuyButton> EndDragButton = delegate { };

    //private TapGestureRecognizer tapGesture;
    //private TapGestureRecognizer doubleTapGesture;
    //private TapGestureRecognizer tripleTapGesture;

    //private LongPressGestureRecognizer longPressGesture;

    //private PanGestureRecognizer panGesture;
    //private void OnValidate()
    //{
        
    //    if (camera == null)
    //    {
    //        camera = Camera.main;
    //    }
    //}

    //private void Start()
    //{

    //    CreateTripleTapGesture();
    //    CreateDoubleTapGesture();
    //    CreateTapGesture();

    //    CreateLongPressGesture();
    //    CreatePanGesture();
    //}

    ////private void TapGestureCallback(GestureRecognizer gesture)
    ////{

    ////    if (gesture.State == GestureRecognizerState.Ended)
    ////    {
    ////        //-----------------------------------------------------------mover a algun lugar para desacoplar
    ////        if (BuyButtonManager.Instance.PlacementMode)
    ////        {

    ////        }
    ////        if (debug)
    ////        {
    ////            Debug.Log($"Tapped at {gesture.FocusX}, {gesture.FocusY}");
    ////        }
    ////        Ray ray = camera.ScreenPointToRay(new Vector3(gesture.FocusX, gesture.FocusY, 0));
    ////        if (Physics.Raycast(ray, out RaycastHit raycastHit, MAX_RAY_DISTANCE, selectableLayers))
    ////        {
    ////            Debug.Log("yo");
    ////            Selectable s = raycastHit.transform.GetComponent<Selectable>();
    ////            if (s != null)
    ////            {
    ////                s.Select();
    ////            }
    ////        }
    ////        else
    ////        {
    ////            SelectionManager.Instance.DeselectAll();
    ////        }
    ////    }
    ////}
    //private void TapGestureCallback(GestureRecognizer gesture)
    //{

    //    if (gesture.State == GestureRecognizerState.Ended)
    //    {
    //        if (debug)
    //            Debug.Log($"Tapped at {gesture.FocusX}, {gesture.FocusY}");
    //        if (BuyButton.SelectedButton != null)
    //        {
    //            PlacementModeTap(gesture);
    //        }
    //        else
    //        {
    //            DefaultTap(gesture);
    //        }

            
    //    }
    //}
    //private void PlacementModeTap(GestureRecognizer gesture)
    //{
    //    Ray ray = camera.ScreenPointToRay(new Vector3(gesture.FocusX, gesture.FocusY, 0));
    //    if (Physics.Raycast(ray, out RaycastHit raycastHit, MAX_RAY_DISTANCE, selectableLayers))
    //    {
    //        Selectable s = raycastHit.transform.GetComponent<Selectable>();
    //        if (s != null)
    //        {
    //            s.Select();
    //        }
    //    }
    //}
    //private void DefaultTap(GestureRecognizer gesture)
    //{
    //    Ray ray = camera.ScreenPointToRay(new Vector3(gesture.FocusX, gesture.FocusY, 0));
    //    if (Physics.Raycast(ray, out RaycastHit raycastHit, MAX_RAY_DISTANCE, selectableLayers))
    //    {
    //        Selectable s = raycastHit.transform.GetComponent<Selectable>();
    //        if (s != null)
    //        {
    //            s.Select();
    //        }
    //    }
    //    else
    //    {
    //        SelectionManager.Instance.DeselectAll();
    //    }
    //}

    //private void CreateTapGesture()
    //{
    //    tapGesture = new TapGestureRecognizer();
    //    tapGesture.ThresholdSeconds = 0.4f;
    //    tapGesture.StateUpdated += TapGestureCallback;
    //    tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;
    //    FingersScript.Instance.AddGesture(tapGesture);
    //}

    //private void DoubleTapGestureCallback(GestureRecognizer gesture)
    //{
    //    if (gesture.State == GestureRecognizerState.Ended)
    //    {
    //        //-------------------------------mover a selectManager
    //        if (debug)
    //            Debug.Log($"Double tapped at {gesture.FocusX}, {gesture.FocusY}");
    //        DefaultDoubleTap(gesture);
    //    }
    //}

    //private void DefaultDoubleTap(GestureRecognizer gesture)
    //{
    //    //en displacment mode deselecciona el boton!
    //    Ray ray = camera.ScreenPointToRay(new Vector3(gesture.FocusX, gesture.FocusY, 0));
    //    if (Physics.Raycast(ray, out RaycastHit hitInfo, MAX_RAY_DISTANCE, movableLayers))
    //    {
    //        SelectionManager.Instance.SetDestinationToAllSelected(hitInfo.point); // por defecto manda un player move. luego al agregar un botton que active el attack move va a ser necesario tener un intermediario en esta red de eventos, el cual de true o false si esta el attack move activo o no.
    //    }
    //}

    //private void CreateDoubleTapGesture()
    //{
    //    doubleTapGesture = new TapGestureRecognizer();
    //    doubleTapGesture.ThresholdSeconds = 0.15f;
    //    doubleTapGesture.NumberOfTapsRequired = 2;
    //    doubleTapGesture.StateUpdated += DoubleTapGestureCallback;
    //    doubleTapGesture.RequireGestureRecognizerToFail = tripleTapGesture;
    //    FingersScript.Instance.AddGesture(doubleTapGesture);
    //}

    //private void CreateTripleTapGesture()
    //{
    //    tripleTapGesture = new TapGestureRecognizer();
    //    tripleTapGesture.NumberOfTapsRequired = 3;
    //    FingersScript.Instance.AddGesture(doubleTapGesture);
    //}

    //private void PanGestureCallBack(GestureRecognizer gesture)
    //{
    //    if (gesture.State == GestureRecognizerState.Began)
    //    {
    //        Vector2 start = new Vector2(gesture.StartFocusX, gesture.StartFocusY);
    //        Ray ray = camera.ScreenPointToRay(start);
    //        //----------------------------------------------------------------------------------------------------------------------------------------------------------el problema es el raycast hacia la ui desde la camara. eso simplemente no funca. estudiar eso dsps de jugar su tf sos.
    //        if (Physics.Raycast(ray, out RaycastHit hitInfo, MAX_RAY_DISTANCE, buyingButtonsLayers))
    //        {
    //            BuyButton button = hitInfo.transform.GetComponent<BuyButton>();
    //            if (button != null)
    //            {
    //                BuyButton.SelectedButton = button;
    //            }
    //            else
    //                Debug.LogError("missing buy button component. Do you forget to put it?");
    //        }
    //    }
    //    if (gesture.State == GestureRecognizerState.Executing)
    //    {
    //        if (debug)
    //        {
    //            Debug.Log($"Pan started at {gesture.StartFocusX}|{gesture.StartFocusY} and is now in {gesture.FocusX}|{ gesture.FocusY}.");
    //        }
    //        if (BuyButton.SelectedButton != null)
    //        {
    //            DragButton(new Vector2(gesture.FocusX, gesture.FocusY), BuyButton.SelectedButton);
    //        }
    //        else
    //            SelectionManager.Instance.RectSelection(new Vector2(gesture.StartFocusX, gesture.StartFocusY), new Vector2(gesture.FocusX, gesture.FocusY));
    //    }
    //    else if (gesture.State == GestureRecognizerState.Ended)
    //    {
    //        if (BuyButton.SelectedButton != null)
    //        {
    //            EndDragButton(new Vector2(gesture.FocusX, gesture.FocusY), BuyButton.SelectedButton);
    //        }
    //        else
    //        {
    //            SelectionManager.Instance.DeselectAll();
    //            SelectionManager.Instance.EndRectSelection(new Vector2(gesture.StartFocusX, gesture.StartFocusY), new Vector2(gesture.FocusX, gesture.FocusY));
    //        }
            
    //    }
    //}

    //private void CreatePanGesture()
    //{
    //    panGesture = new PanGestureRecognizer();
    //    panGesture.StateUpdated += PanGestureCallBack;
    //    FingersScript.Instance.AddGesture(panGesture);
    //}

    //private void CreateLongPressGesture()
    //{
    //    //--------------------------------------------------------------------------------------------pyp
    //}
    #endregion
}
