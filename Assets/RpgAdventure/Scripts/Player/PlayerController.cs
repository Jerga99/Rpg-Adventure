using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;
        public Camera cam;

        private Rigidbody m_Rb;
        private Vector3 m_Movement;
        //private Quaternion m_Rotation;

        private void Start()    
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            m_Movement.Set(horizontalInput, 0, verticalInput);
            m_Movement.Normalize();

            //Vector3 desiredForward = Vector3.RotateTowards(
            //    transform.forward,
            //    m_Movement,
            //    rotationSpeed * Time.fixedDeltaTime,
            //    0);

            //Quaternion rotation = Quaternion.LookRotation(desiredForward);


            m_Rb.MovePosition(m_Rb.position + m_Movement * speed * Time.fixedDeltaTime);

            //m_Rb.MoveRotation(rotation);
        }
    }
}
