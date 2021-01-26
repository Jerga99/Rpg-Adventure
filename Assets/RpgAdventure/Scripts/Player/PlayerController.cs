using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://assetstore.unity.com/packages/3d/characters/medieval-cartoon-warriors-90079

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;

        private CharacterController m_ChController;
        private Vector3 m_Movement;
        private Camera m_MainCamera;

        private void Awake()    
        {
            m_ChController = GetComponent<CharacterController>();
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

            m_ChController.Move(targetDirection.normalized * speed * Time.fixedDeltaTime);
            m_ChController.transform.rotation = Quaternion.Euler(0, camRotation.eulerAngles.y, 0);
        }
    }
}
