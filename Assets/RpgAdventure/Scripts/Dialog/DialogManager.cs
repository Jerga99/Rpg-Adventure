using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject dialogUI;
        public Text dialogHeaderText;

        private void Awake()
        {
            dialogUI.SetActive(false);
        }

        private void Update()
        {
            if (PlayerInput.Instance != null &&
                PlayerInput.Instance.IsTalk)
            {
                StartDialog();
            }
        }

        private void StartDialog()
        {
            dialogUI.SetActive(true);
            dialogHeaderText.text = "Just Testing!";
        }
    }
}
