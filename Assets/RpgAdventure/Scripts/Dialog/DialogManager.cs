using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {

        private void Update()
        {
            if (PlayerInput.Instance != null &&
                PlayerInput.Instance.IsTalk)
            {
                Debug.Log("Starting Dialog!");
            }
        }
    }
}
