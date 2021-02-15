using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject dialogUI;
        public Text dialogHeaderText;

        private bool m_HasActiveDialog;

        private void Awake()
        {
            dialogUI.SetActive(false);
        }

        private void Update()
        {
            if (!m_HasActiveDialog &&
                PlayerInput.Instance != null &&
                PlayerInput.Instance.OptionClickTarget != null)
            {
                if (PlayerInput.Instance.OptionClickTarget.CompareTag("QuestGiver"))
                {
                    var distanceToTarget = Vector3.Distance(
                    PlayerInput.Instance.transform.position,
                    PlayerInput.Instance.OptionClickTarget.transform.position);

                    if (distanceToTarget < 2.0f)
                    {
                        Debug.Log("Starting Dialog!");
                        StartDialog();
                    }
                }
            }
        }

        private void StartDialog()
        {
            m_HasActiveDialog = true;
            dialogUI.SetActive(true);
            dialogHeaderText.text = "Just Testing!";
        }
    }
}
