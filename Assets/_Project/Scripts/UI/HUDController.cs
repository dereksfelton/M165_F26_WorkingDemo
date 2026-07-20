using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Thin wrapper around UI Toolkit operations for the HUD. Exposes
/// public methods that GameEventListener responses can call from
/// the Inspector, without callers needing any UI Toolkit knowledge.
/// Built against PanelRenderer (Unity 6.5+), not the obsolete UIDocument
/// component — see the Rationale note below for why.
/// </summary>
[RequireComponent(typeof(PanelRenderer))]
public class HUDController : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private VisualElement youWinPanel;

    // PanelRenderer does not expose a freely-accessible rootVisualElement
    // the way the older UIDocument did. Instead, the root is only valid
    // inside a registered reload callback, fired whenever the UI is
    // (re)built. Registration happens in OnEnable/OnDisable, mirroring the
    // same enable/disable pairing already used by GameEventListener.
    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    // Fires once when the UI is built (and again on any later rebuild).
    // This is the only place it's safe to look up elements by name —
    // caching youWinPanel here avoids re-querying the visual tree every
    // time the panel needs to show or hide. Also hides the panel
    // immediately, since it should not be visible by default.
    private void OnUIReload(PanelRenderer renderer, VisualElement root, int version)
    {
        youWinPanel = root.Q<VisualElement>("YouWinPanel");
        youWinPanel.style.display = DisplayStyle.None;
    }

    // Called via GameEventListener when the player reaches the goal.
    public void ShowYouWinPanel()
    {
        youWinPanel.style.display = DisplayStyle.Flex;
    }

    // Available for future use (e.g., resetting the HUD on replay) —
    // not wired to anything in Module 1.
    public void HideYouWinPanel()
    {
        youWinPanel.style.display = DisplayStyle.None;
    }
}