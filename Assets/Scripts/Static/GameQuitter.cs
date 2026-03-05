using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class GameQuitter
{
    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
