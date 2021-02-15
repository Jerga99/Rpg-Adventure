using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject dialogUI;
        public Text dialogHeaderText;

        private PlayerInput m_Player;
        private GameObject m_Npc;
        private Dialog m_ActiveDialog;

        public bool HasActiveDialog { get { return m_ActiveDialog != null; } }

        private void Start()
        {
            m_Player = PlayerInput.Instance;
        }

        private void Update()
        {
            if (!HasActiveDialog &&
                m_Player != null &&
                m_Player.OptionClickTarget != null)
            {
                if (m_Player.OptionClickTarget.CompareTag("QuestGiver"))
                {
                    m_Npc = m_Player.OptionClickTarget.gameObject;

                    var distanceToTarget = Vector3.Distance(
                    m_Player.transform.position,
                    m_Npc.transform.position);

                    if (distanceToTarget < 2.0f)
                    {
                        StartDialog();
                    }
                }
            }
        }

        private void StartDialog()
        {
            m_ActiveDialog = m_Npc.GetComponent<QuestGiver>().dialog;
            dialogUI.SetActive(true);
            dialogHeaderText.text = m_Npc.name;
        }
    }
}
