using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get { return s_Instance; } }
        public bool isPlayerControllerInputBlocked;
        public float distanceToInteractWithNpc = 2.0f;

        private static PlayerInput s_Instance;
        private Vector3 m_Movement;
        private bool m_IsAttack;
        private Collider m_OptionClickTarget;

        public Collider OptionClickTarget { get { return m_OptionClickTarget; } }
        public Vector3 MoveInput
        {
            get
            {
                if (isPlayerControllerInputBlocked)
                {
                    return Vector3.zero;
                }
                return m_Movement;
            }
        }

        public bool IsAttack
        {
            get
            {
                return !isPlayerControllerInputBlocked && m_IsAttack;
            }
        }
        public bool IsMoveInput { get { return !Mathf.Approximately(MoveInput.magnitude, 0); } }

        private void Awake()
        {
            s_Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            m_Movement.Set(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );

            bool isLeftMouseClick = Input.GetMouseButtonDown(0);
            bool isRighMouseClick = Input.GetMouseButtonDown(1);

            if (isLeftMouseClick)
            {
                HandleLeftMouseBtnDown();
            }

            if (isRighMouseClick)
            {
                HandleRightMouseBtnDown();
            }
        }

        private void HandleLeftMouseBtnDown()
        {
            if (!m_IsAttack && !IsPointerOverUiElement())
            {
                StartCoroutine(TriggerAttack());
            }
        }

        private bool IsPointerOverUiElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
        }

        private void HandleRightMouseBtnDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

            if (hasHit)
            {
                StartCoroutine(TriggerOptionTarget(hit.collider));
            }
        }

        private IEnumerator TriggerOptionTarget(Collider other)
        {
            m_OptionClickTarget = other;
            yield return new WaitForSeconds(0.03f);
            m_OptionClickTarget = null;
        }

        private IEnumerator TriggerAttack()
        {
            m_IsAttack = true;
            yield return new WaitForSeconds(0.03f);
            m_IsAttack = false;
        }
    }
}
