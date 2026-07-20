using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A GameEvent variant that carries an int payload.
/// Same registration pattern as GameEvent, but Raise() takes a value
/// and every listener receives it.
/// </summary>
[CreateAssetMenu(menuName = "Events/Int Game Event", fileName = "NewIntGameEvent")]
public class IntGameEvent : ScriptableObject
{
    private readonly List<IntGameEventListener> listeners = new List<IntGameEventListener>();

    // Notifies every registered listener, passing the same int value to
    // each. Backward iteration mirrors GameEvent.Raise() for the same
    // mid-loop-unregister safety.
    public void Raise(int value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    // Adds a listener so it receives future Raise(int) calls. Called
    // automatically by IntGameEventListener.OnEnable().
    public void RegisterListener(IntGameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    // Removes a listener so it stops receiving Raise(int) calls. Called
    // automatically by IntGameEventListener.OnDisable().
    public void UnregisterListener(IntGameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}