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

        private PlayerInput m_PlayerInput;
        private CharacterController m_ChController;
        private Camera m_MainCamera;

        private void Awake()    
        {
            m_ChController = GetComponent<CharacterController>();
            m_PlayerInput = GetComponent<PlayerInput>();
            m_MainCamera = Camera.main;
        }

        void FixedUpdate()
        {
            Vector2 moveInput = m_PlayerInput.MoveInput;
            Vector3 localMovementInput = new Vector3(moveInput.x, 0, moveInput.y);

            Quaternion camRotation = m_MainCamera.transform.rotation;
            Vector3 targetDirection = camRotation * localMovementInput;
            targetDirection.y = 0;

            m_ChController.Move(targetDirection.normalized * speed * Time.fixedDeltaTime);
            m_ChController.transform.rotation = Quaternion.Euler(0, camRotation.eulerAngles.y, 0);
        }
    }
}
