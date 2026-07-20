using UnityEngine;
using UnityEngine.Events;

// Concrete UnityEvent<int> subclass — required because Unity's Inspector
// serializer can't draw a generic UnityEvent<T> directly. This class adds
// no behavior of its own; it exists only to satisfy that requirement.
[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }

public class IntGameEventListener : MonoBehaviour
{
    [SerializeField] private IntGameEvent gameEvent;
    [SerializeField] private UnityIntEvent response;

    // Subscribes to the assigned IntGameEvent as soon as this component
    // becomes active. Guarded against gameEvent being unassigned, for the
    // same reason as GameEventListener.cs — OnEnable() runs on scene load
    // regardless of whether the field has been wired yet.
    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    // Unsubscribes when this component is disabled or destroyed. Same
    // null guard as OnEnable().
    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    // Called by IntGameEvent.Raise(int) on every registered listener.
    // Passes the value through to whatever methods were wired into the
    // Response field in the Inspector.
    public void OnEventRaised(int value)
    {
        response.Invoke(value);
    }
}