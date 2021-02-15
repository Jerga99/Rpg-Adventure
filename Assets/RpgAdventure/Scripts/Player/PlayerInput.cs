using System.Collections;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get { return s_Instance; } }
        public float distanceToInteractWithNpc = 2.0f;

        private static PlayerInput s_Instance;
        private Vector3 m_Movement;
        private bool m_IsAttack;
        private bool m_IsTalk;

        public Vector3 MoveInput { get { return m_Movement; } }
        public bool IsMoveInput { get { return !Mathf.Approximately(MoveInput.magnitude, 0); } }
        public bool IsAttack { get { return m_IsAttack; } }
        public bool IsTalk { get { return m_IsTalk; } }

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
            if (!m_IsAttack)
            {
                StartCoroutine(TriggerAttack());
            }
        }

        private void HandleRightMouseBtnDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

            if (hasHit && hit.collider.CompareTag("QuestGiver"))
            {
                var distanceToTarget = (transform.position - hit.transform.position).magnitude;

                if (distanceToTarget <= distanceToInteractWithNpc)
                {
                    StartCoroutine(TriggerTalk());
                }
            }
        }

        private IEnumerator TriggerTalk()
        {
            m_IsTalk = true;
            yield return new WaitForSeconds(0.03f);
            m_IsTalk = false;
        }

        private IEnumerator TriggerAttack()
        {
            m_IsAttack = true;
            yield return new WaitForSeconds(0.03f);
            m_IsAttack = false;
        }
    }
}
