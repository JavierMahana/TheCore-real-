using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Selectable : MonoBehaviour
{
    //hay que retrabajarlo.

    private bool onApplicationQuit = false;
    [SerializeField]
    private GameObject inSelectionKnob;
    
    public static Action<Selectable> OnSelect = delegate { };
    public static Action<Selectable> OnDeselect = delegate { };

    private void OnEnable()
    {
        SelectionManager.Instance.EndOfRectSelection += SelectIfWithinSelectionBounds;
    }
    private void OnApplicationQuit()
    {
        onApplicationQuit = true;
    }
    private void OnDisable()
    {
        if (onApplicationQuit)
        {
            return;
        }
        SelectionManager.Instance.EndOfRectSelection -= SelectIfWithinSelectionBounds;
    }

    private void Awake()
    {
        inSelectionKnob.SetActive(false);
    }
    public void Select()
    {
        inSelectionKnob.SetActive(true);
        OnSelect(this);
    }
    public void Deselect()
    {
        inSelectionKnob.SetActive(false);
        OnDeselect(this);
    }

    private void SelectIfWithinSelectionBounds(Bounds selectionBoundsInViewPort)
    {
        if (selectionBoundsInViewPort.Contains(SelectionManager.Instance.Camera.WorldToViewportPoint(transform.position)))
        {
            Select();
        }
    }

}
