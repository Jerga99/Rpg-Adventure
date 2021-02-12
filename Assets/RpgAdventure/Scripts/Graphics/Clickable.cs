using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class Clickable : MonoBehaviour
    {
        public Texture2D questionCursor;

        private void OnMouseEnter()
        {
            Debug.Log("OnMouseEnter");
        }

        private void OnMouseExit()
        {
            Debug.Log("OnMouseExit");
        }
    }
}
