using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A signal-only event implemented as a ScriptableObject asset.
/// Raise() notifies every registered listener. Carries no data —
/// it only announces that something happened.
/// </summary>
[CreateAssetMenu(menuName = "Events/Game Event", fileName = "NewGameEvent")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> listeners = new List<GameEventListener>();

    // Notifies every registered listener, in reverse order, that this
    // event has occurred. Iterating backward protects against a listener
    // unregistering itself mid-loop (see Rationale below).
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    // Adds a listener so it receives future Raise() calls. Called
    // automatically by GameEventListener.OnEnable() — not invoked directly
    // by students.
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    // Removes a listener so it stops receiving Raise() calls. Called
    // automatically by GameEventListener.OnDisable().
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}