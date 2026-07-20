using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Inspector-wirable listener for FloatGameEvent — the float counterpart
/// to IntGameEventListener, following the identical pattern.
/// </summary>
[System.Serializable]
public class UnityFloatEvent : UnityEvent<float> { }

public class FloatGameEventListener : MonoBehaviour
{
    [SerializeField] private FloatGameEvent gameEvent;
    [SerializeField] private UnityFloatEvent response;

    // Subscribes to the assigned FloatGameEvent as soon as this component
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

    // Called by FloatGameEvent.Raise(float) on every registered listener.
    // Passes the value through to whatever methods were wired into the
    // Response field in the Inspector.
    public void OnEventRaised(float value)
    {
        response.Invoke(value);
    }
}