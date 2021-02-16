using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public float maxDialogDistance;
        public GameObject dialogUI;
        public Text dialogHeaderText;
        public Text dialogAnswerText;
        public GameObject dialogOptionList;
        public Button dialogOptionPrefab;

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
            dialogAnswerText.text = m_ActiveDialog.welcomeText;
            dialogUI.SetActive(true);

            CreateDialogMenu();
        }

        private void CreateDialogMenu()
        {
            var queries = Array.FindAll(m_ActiveDialog.queries, query => !query.isAsked);

            foreach (var query in queries)
            {
                var dialogOption = CreateDialogOption(query.text);
            }
        }

        private Button CreateDialogOption(string optionText)
        {
            Button buttonInstance = Instantiate(dialogOptionPrefab, dialogOptionList.transform);
            buttonInstance.GetComponentInChildren<Text>().text = optionText;

            return buttonInstance;
        }

        private void StopDialog()
        {
            m_Npc = null;
            m_ActiveDialog = null;
            dialogUI.SetActive(false);
        }
    }
}
