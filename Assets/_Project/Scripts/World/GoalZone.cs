using UnityEngine;

/// <summary>
/// Raises a GameEvent when the player enters this trigger.
/// Demonstrates the minimal pattern: detect, then raise — no
/// knowledge of what (if anything) responds to the event.
/// </summary>
public class GoalZone : MonoBehaviour
{
    [SerializeField] private GameEvent onPlayerReachedGoal;

    // Fires when anything enters this trigger. Filters by tag rather than
    // by component type, so this script never needs to know anything
    // about how the player is implemented.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerReachedGoal.Raise();
        }
    }
}