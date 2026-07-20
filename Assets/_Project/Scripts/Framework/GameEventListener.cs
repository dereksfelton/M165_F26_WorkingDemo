using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Inspector-wirable component that listens for a GameEvent and
/// invokes a UnityEvent response when it's raised. This is the
/// component students drag GameEvent assets onto and wire responses
/// for entirely within the Inspector — no code required.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;

    // Subscribes to the assigned GameEvent as soon as this component
    // becomes active, so it's ready to respond even if the event fires
    // immediately. Guarded against gameEvent being unassigned — this
    // course deliberately ships some GameEventListener components with an
    // empty Game Event field (e.g., the HUD listener in Module 1, wired
    // live by students rather than pre-configured), and OnEnable() runs
    // automatically the instant the scene loads, before anyone has a
    // chance to assign anything. Without this guard, every unwired
    // listener throws a NullReferenceException on scene load.
    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    // Unsubscribes when this component is disabled or destroyed, so a
    // disabled/pooled object never receives a response call meant for
    // an active one. Same null guard as OnEnable(), for the same reason.
    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    // Called by GameEvent.Raise() on every registered listener. Invokes
    // whatever methods were wired into the Response field in the Inspector.
    public void OnEventRaised()
    {
        response.Invoke();
    }
}