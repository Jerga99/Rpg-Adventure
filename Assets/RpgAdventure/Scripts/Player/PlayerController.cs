using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;

        private Rigidbody m_Rb;
        private Vector3 m_Movement;
        private Camera m_MainCamera;

        private void Awake()    
        {
            m_Rb = GetComponent<Rigidbody>();
            m_MainCamera = Camera.main;
        }

        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            m_Movement.Set(horizontalInput, 0, verticalInput);

            Quaternion camRotation = m_MainCamera.transform.rotation;
            Vector3 targetDirection = camRotation * m_Movement;
            targetDirection.y = 0;
            
            m_Rb.MovePosition(m_Rb.position + targetDirection.normalized * speed * Time.fixedDeltaTime);
            m_Rb.MoveRotation(Quaternion.Euler(0, camRotation.eulerAngles.y, 0));
        }
    }
}
