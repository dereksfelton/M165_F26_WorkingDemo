using UnityEngine;

/// <summary>
/// Game-level state only — not player data (see the State SO
/// granularity rule in StarterProjectScaffold.md). Ships empty in
/// Module 1; first read/written when the Main Menu and pause flow
/// are built in later modules.
/// </summary>
public enum GamePhase
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

[CreateAssetMenu(menuName = "State/Game State", fileName = "GameStateSO")]
public class GameStateSO : ScriptableObject
{
    public GamePhase currentPhase;
}