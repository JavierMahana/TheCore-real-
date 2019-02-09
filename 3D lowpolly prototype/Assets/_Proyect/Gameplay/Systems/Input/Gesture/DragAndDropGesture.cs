using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class DragAndDropGesture : GestureRecognizer
{

    protected override void TouchesBegan(System.Collections.Generic.IEnumerable<GestureTouch> touches)
    {
        //fail if any touch is in the gameObjects that trigger fail.
        foreach (GestureTouch touch in touches)
        {
            if (FingersScript.Instance.gameObjectsForTouch.TryGetValue(touch.Id, out List<GameObject> results))
            {
                foreach (GameObject r in results)
                {
                    foreach (GameObject failObject in failIfStartInTheseObjects)
                    {
                        if (failObject.Equals(r))
                        {
                            SetState(GestureRecognizerState.Failed);
                            return;
                        }
                    }
                    
                }
            }
        }
        CalculateFocus(CurrentTrackedTouches, true);
        SetState(GestureRecognizerState.Executing);
    }

    protected override void TouchesMoved()
    {
        CalculateFocus(CurrentTrackedTouches);
        if (State == GestureRecognizerState.Began || State == GestureRecognizerState.Executing)
        {
            SetState(GestureRecognizerState.Executing);
        }
    }

    protected override void TouchesEnded()
    {
        if (State == GestureRecognizerState.Began || State == GestureRecognizerState.Executing)
        {
            CalculateFocus(CurrentTrackedTouches);
            SetState(GestureRecognizerState.Ended);
        }
    }

    public DragAndDropGesture(GameObject[] failIfStartInTheseObjects)
    {
        this.failIfStartInTheseObjects = failIfStartInTheseObjects;
    }

    private GameObject[] failIfStartInTheseObjects; 
}
