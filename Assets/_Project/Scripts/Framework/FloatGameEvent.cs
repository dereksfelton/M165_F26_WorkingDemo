using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A GameEvent variant that carries a float payload. Same registration
/// pattern as GameEvent, but Raise() takes a value and every listener
/// receives it. The float counterpart to IntGameEvent — used for
/// continuous quantities like damage amounts rather than discrete counts.
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Game Event", fileName = "NewFloatGameEvent")]
public class FloatGameEvent : ScriptableObject
{
    private readonly List<FloatGameEventListener> listeners = new List<FloatGameEventListener>();

    // Notifies every registered listener, passing the same float value to
    // each. Backward iteration mirrors GameEvent.Raise() for the same
    // mid-loop-unregister safety.
    public void Raise(float value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    // Adds a listener so it receives future Raise(float) calls. Called
    // automatically by FloatGameEventListener.OnEnable().
    public void RegisterListener(FloatGameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    // Removes a listener so it stops receiving Raise(float) calls. Called
    // automatically by FloatGameEventListener.OnDisable().
    public void UnregisterListener(FloatGameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}