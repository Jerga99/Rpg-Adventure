using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;

        private Rigidbody m_Rb;
        private Vector3 m_Movement;

        private void Start()    
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            m_Movement = new Vector3(horizontalInput, 0, verticalInput);
            m_Movement.Normalize();

            m_Rb.MovePosition(m_Rb.position + m_Movement * speed * Time.fixedDeltaTime);
        }
    }
}
