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
        //private Vector3 m_Movement;
        //private Quaternion m_Rotation;

        private void Start()    
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.GetAxis("Horizontal");
            dir.z = Input.GetAxis("Vertical");

            if (dir == Vector3.zero)
            {
                return;
            }

            Vector3 targetDirection = cam.transform.rotation * dir;
            targetDirection.y = 0;

            if (dir.z >= 0)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(targetDirection),
                    0.1f);
            }

            m_Rb.MovePosition(m_Rb.position + targetDirection.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
