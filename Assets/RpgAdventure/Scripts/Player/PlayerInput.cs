using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 m_Movement;

        public Vector2 MoveInput
        {
            get
            {
                return m_Movement;
            }
        }

        // Update is called once per frame
        void Update()
        {
            m_Movement.Set(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
        }
    }
}
