using UnityEngine;

/// <summary>
/// A minimal, reusable listener-response utility: logs a configurable
/// message to the Console when its LogMessage() method is called by a
/// GameEventListener's Response. Built specifically so this module's
/// hazard-zone demo can have a wired, working response without
/// borrowing HUDController or inventing real damage logic ahead of
/// Module 17 — PlayerHealthSO stays untouched today. May get reused in
/// later modules as a lightweight "verify this event fires before its
/// real response exists" tool.
/// </summary>
public class DebugLog : MonoBehaviour
{
    [SerializeField] private string message;

    // Wired to a GameEventListener's Response field in the Inspector.
    // No parameters — GameEventListener's Response is a plain UnityEvent,
    // not a typed one, so this method takes nothing and reads its
    // message from the Inspector-set field instead.
    public void LogMessage()
    {
        Debug.Log(message);
    }
}
