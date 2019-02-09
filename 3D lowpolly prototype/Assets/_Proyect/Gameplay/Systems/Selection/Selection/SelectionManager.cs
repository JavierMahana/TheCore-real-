using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class SelectionManager : ScriptableObject
{
    /// <summary>
    /// Bounds with the viewport coordinates of the selection rect.
    /// </summary>
    public Action<Bounds> EndOfRectSelection = delegate { };

    private HashSet<Selectable> selectedList = new HashSet<Selectable>();



    private void Awake()
    {
        Selectable.OnSelect += AddToSelectedList;
        Selectable.OnDeselect += RemoveFromSelectedList;
    }

    private void AddToSelectedList(Selectable selectable)
    {
        selectedList.Add(selectable);
    }
    private void RemoveFromSelectedList(Selectable selectable)
    {
        //selectedList.Remove(selectable);
    }

    //esto no tiene sentido que este acá.
    public void SetDestinationToAllSelected(Vector3 destination)
    {
        foreach (Selectable selected in selectedList)
        {
            AutonomousMovement m = selected.GetComponent<AutonomousMovement>();
            if (m != null)
            {
                m.ChangeDestination(destination);
            }
            else
                Debug.Log($"AutonomusMovement missing", selected);
        }
    }

    public void DeselectAll()
    {
        foreach (Selectable item in selectedList)
        {
            
            item.Deselect();
            
        }
        selectedList.Clear();
    }
    
    public void EndRectSelection(Vector2 screenPos1, Vector2 screenPos2)
    {
        Bounds viewportBounds = UtilityGUI.GetViewportBoundsFromScreenPoints(CameraUtility.Instance.MainCamera, screenPos1, screenPos2);
        EndOfRectSelection(viewportBounds);
        isSelecting = false;
    }

}
