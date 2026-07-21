using UnityEngine;

/// <summary>
/// Raises a GameEvent when the player enters this trigger — a second,
/// independent example of the same pattern GoalZone.cs established in
/// Module 1. Deliberately built by reading and adapting GoalZone.cs
/// rather than written from scratch, reinforcing "read an existing
/// pattern, don't reinvent it."
/// </summary>
public class DamageZone : MonoBehaviour
{
    [SerializeField] private GameEvent onPlayerEnteredHazard;

    // Fires when anything enters this trigger. Filters by tag, exactly
    // like GoalZone.cs — this script never needs to know anything about
    // how the player is implemented, only that the colliding object is
    // tagged Player.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnteredHazard.Raise();
        }
    }
}