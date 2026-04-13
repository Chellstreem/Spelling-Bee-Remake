using GameStates;
using UnityEngine;

public class CursorController
{
    private readonly GameStateController stateController;
    private readonly CursorMode cursorMode = CursorMode.Auto;

    private Texture2D _currentCursor;
    private Vector2 _hotspot;

    public CursorController(GameStateController stateController)
    {
        this.stateController = stateController;
        stateController.OnStateChanged += OnStateChanged;
    }

    public void SetActive(bool isActive) => Cursor.visible = isActive;

    public void SetCursor(Texture2D cursorTexture, Vector2 hotspot)
    {
        _currentCursor = cursorTexture;
        _hotspot = hotspot;

        Cursor.SetCursor(_currentCursor, _hotspot, cursorMode);
    }

    public void ResetCursor()
    {
        _currentCursor = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Refresh() => Cursor.SetCursor(_currentCursor, _hotspot, cursorMode);

    private void OnStateChanged()
    {
        bool isVisible = stateController.CurrentState.Definition.IsCursorVisible;
        SetActive(isVisible);
    }

    public void Dispose() => stateController.OnStateChanged -= OnStateChanged;
}

