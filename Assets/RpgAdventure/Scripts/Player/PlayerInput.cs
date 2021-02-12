using System.Collections;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
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

        private void Start()
        {
            void TestMethod(out int number)
            {
                number = 200;
            }

            TestMethod(out int testNumber);

            Debug.Log(testNumber);
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

            if (isLeftMouseClick && !m_IsAttack)
            {
                StartCoroutine(AttackAndWait());
            }

            if (isRighMouseClick)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

                if (hasHit)
                {
                    Debug.Log("Has Hit With: " + hit.collider.name);
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
