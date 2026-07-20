using UnityEngine;

/// <summary>
/// The player's score "whiteboard." Ships empty in Module 1; first
/// written to and read from starting with the Module 11 pickup.
/// </summary>
[CreateAssetMenu(menuName = "State/Score", fileName = "ScoreSO")]
public class ScoreSO : ScriptableObject
{
    public int currentScore;
    public int highScore;
}