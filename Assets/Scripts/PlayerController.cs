using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;

        private Vector3 m_Movement;

        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            m_Movement = new Vector3(horizontalInput, 0, verticalInput);
            m_Movement.Normalize();

            transform.Translate(m_Movement * speed * Time.fixedDeltaTime);
        }
    }
}
