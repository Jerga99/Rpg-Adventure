using System.Collections;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        public float distanceToInteractWithNpc = 2.0f;

        private Vector3 m_Movement;
        private bool m_IsAttack;

        public Vector3 MoveInput
        {
            get
            {
                return m_Movement;
            }
        }

        public bool IsMoveInput
        {
            get
            {
                return !Mathf.Approximately(MoveInput.magnitude, 0);
            }
        }

        public bool IsAttack
        {
            get
            {
                return m_IsAttack;
            }
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
                StartCoroutine(AttackAndWait());
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
                    Debug.Log("Hello Emilia!");
                }



            }
        }

        private IEnumerator AttackAndWait()
        {
            m_IsAttack = true;
            yield return new WaitForSeconds(0.03f);
            m_IsAttack = false;
        }
    }
}
