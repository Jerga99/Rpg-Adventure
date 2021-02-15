using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public float maxDialogDistance;
        public GameObject dialogUI;
        public Text dialogHeaderText;
        public Text dialogWelcomeText;

        private PlayerInput m_Player;
        private QuestGiver m_Npc;

        private Dialog m_ActiveDialog;

        public bool HasActiveDialog { get { return m_ActiveDialog != null; } }
        public float DialogDistance
        {
            get
            {
                return Vector3.Distance(
                    m_Player.transform.position,
                    m_Npc.transform.position);
            }
        }

        private void Start()
        {
            m_Player = PlayerInput.Instance;
        }

        private void Update()
        {
            if (!HasActiveDialog &&
                m_Player.OptionClickTarget != null)
            {
                if (m_Player.OptionClickTarget.CompareTag("QuestGiver"))
                {
                    m_Npc = m_Player.OptionClickTarget.GetComponent<QuestGiver>();

                    if (DialogDistance < maxDialogDistance)
                    {
                        StartDialog();
                    }
                }
            }

            if (HasActiveDialog && DialogDistance > maxDialogDistance + 1.0f)
            {
                StopDialog();
            }
        }

        private void StartDialog()
        {
            m_ActiveDialog = m_Npc.dialog;
            dialogHeaderText.text = m_Npc.name;
            dialogWelcomeText.text = m_ActiveDialog.welcomeText;
            dialogUI.SetActive(true);
        }

        private void StopDialog()
        {
            m_Npc = null;
            m_ActiveDialog = null;
            dialogUI.SetActive(false);
        }
    }
}
