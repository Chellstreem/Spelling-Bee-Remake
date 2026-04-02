using UnityEngine;

namespace UserInterface
{
    public class CursorController
    {
        private Texture2D _currentCursor;
        private Vector2 _hotspot;
        private CursorMode _cursorMode = CursorMode.Auto;

        /// <summary>
        /// Активировать/деактивировать курсор
        /// </summary>
        public void SetActive(bool isActive, bool lockCursor = false)
        {
            Cursor.visible = isActive;

            Cursor.lockState = lockCursor
                ? CursorLockMode.Locked
                : CursorLockMode.None;
        }

        /// <summary>
        /// Установить кастомный курсор
        /// </summary>
        public void SetCursor(Texture2D cursorTexture, Vector2 hotspot)
        {
            _currentCursor = cursorTexture;
            _hotspot = hotspot;

            Cursor.SetCursor(_currentCursor, _hotspot, _cursorMode);
        }

        /// <summary>
        /// Сбросить курсор на дефолтный
        /// </summary>
        public void ResetCursor()
        {
            _currentCursor = null;
            Cursor.SetCursor(null, Vector2.zero, _cursorMode);
        }

        /// <summary>
        /// Повторно применить текущий курсор (например после смены сцены)
        /// </summary>
        public void Refresh()
        {
            Cursor.SetCursor(_currentCursor, _hotspot, _cursorMode);
        }
    }
}

