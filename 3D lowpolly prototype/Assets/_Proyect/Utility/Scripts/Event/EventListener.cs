using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;



    public void OnEventRaised()
    {
        Response.Invoke();
    }

    private void OnEnable()
    {
        Event.AddListener(this);
    }
    private void OnDisable()
    {
        Event.RemoveListener(this);
    }
}
