using UnityEngine;

/// <summary>
/// The player's health "whiteboard" — a ScriptableObject asset any
/// script can read or write without knowing which other scripts also
/// use it. Ships empty in Module 1; not populated or consumed by any
/// script until the Module 17/18 damage loop.
/// </summary>
[CreateAssetMenu(menuName = "State/Player Health", fileName = "PlayerHealthSO")]
public class PlayerHealthSO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
}