using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace RpgAdventure
{
    public class DialogManager : MonoBehaviour
    {
        public float timeToShowOptions = 2.0f;
        public float maxDialogDistance;
        public GameObject dialogUI;
        public Text dialogHeaderText;
        public Text dialogAnswerText;
        public GameObject dialogOptionList;
        public Button dialogOptionPrefab;

        private PlayerInput m_Player;
        private QuestGiver m_Npc;
        private Dialog m_ActiveDialog;
        private float m_OptionTopPosition;
        private float m_TimerToShowOptions;
        private bool m_ForceDialogQuit;

        const float c_DistanceBetweenOption = 32.0f;

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

            if (m_TimerToShowOptions > 0)
            {
                m_TimerToShowOptions += Time.deltaTime;

                if (m_TimerToShowOptions >= timeToShowOptions)
                {
                    m_TimerToShowOptions = 0;

                    if (m_ForceDialogQuit)
                    {
                        StopDialog();
                    }
                    else
                    {
                        DisplayDialogOptions();
                    }
                }
            }
        }

        private void StartDialog()
        {
            m_ActiveDialog = m_Npc.dialog;
            dialogHeaderText.text = m_Npc.name;
            dialogUI.SetActive(true);

            ClearDialogOptions();
            DisplayAnswerText(m_ActiveDialog.welcomeText);
            TriggerDialogOptions();
        }

        private void DisplayAnswerText(string answerText)
        {
            dialogAnswerText.gameObject.SetActive(true);
            dialogAnswerText.text = answerText;
        }

        private void DisplayDialogOptions()
        {
            HideAnswerText();
            CreateDialogMenu();
        }

        private void TriggerDialogOptions()
        {
            m_TimerToShowOptions = 0.001f;
        }

        private void HideAnswerText()
        {
            dialogAnswerText.gameObject.SetActive(false);
        }

        private void CreateDialogMenu()
        {
            m_OptionTopPosition = 0;
            var queries = Array.FindAll(m_ActiveDialog.queries, query => !query.isAsked);

            foreach (var query in queries)
            {
                m_OptionTopPosition += c_DistanceBetweenOption;
                var dialogOption = CreateDialogOption(query.text);
                RegisterOptionClickHandler(dialogOption, query);
            }
        }

        private Button CreateDialogOption(string optionText)
        {
            Button buttonInstance = Instantiate(dialogOptionPrefab, dialogOptionList.transform);
            buttonInstance.GetComponentInChildren<Text>().text = optionText;

            RectTransform rt = buttonInstance.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, m_OptionTopPosition, rt.rect.height);

            return buttonInstance;
        }

        private void RegisterOptionClickHandler(Button dialogOption, DialogQuery query)
        {
            EventTrigger trigger = dialogOption.gameObject.AddComponent<EventTrigger>();
            var pointerDown = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };

            pointerDown.callback.AddListener((e) =>
            {
                if (!String.IsNullOrEmpty(query.answer.questId))
                {
                    m_Player.GetComponent<QuestLog>().AddQuest(m_Npc.quest);
                }

                if (query.answer.forceDialogQuit)
                {
                    m_ForceDialogQuit = true;
                }

                if (!query.isAlwaysAsked)
                {
                    query.isAsked = true;
                }

                ClearDialogOptions();
                DisplayAnswerText(query.answer.text);
                TriggerDialogOptions();
            });

            trigger.triggers.Add(pointerDown);
        }

        private void StopDialog()
        {
            m_Npc = null;
            m_ActiveDialog = null;
            m_TimerToShowOptions = 0;
            m_ForceDialogQuit = false;
            dialogUI.SetActive(false);
        }

        private void ClearDialogOptions()
        {
            foreach (Transform child in dialogOptionList.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
