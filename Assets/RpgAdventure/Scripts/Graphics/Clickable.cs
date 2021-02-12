using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class Clickable : MonoBehaviour
    {
        public Texture2D questionCursor;
        public CursorMode cursorMode = CursorMode.Auto;

        private void OnMouseEnter()
        {
            Vector2 hotspot = new Vector2(questionCursor.width / 2, questionCursor.height / 2);
            Cursor.SetCursor(questionCursor, hotspot, cursorMode);
        }

        private void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
