using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://assetstore.unity.com/packages/3d/characters/medieval-cartoon-warriors-90079

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float maxForwardSpeed = 8.0f;
        public float rotationSpeed;

        private PlayerInput m_PlayerInput;
        private CharacterController m_ChController;
        private Animator m_Animator;
        private Camera m_MainCamera;

        private float m_DesiredForwardSpeed;
        private float m_ForwardSpeed;

        private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");

        private void Awake()    
        {
            m_ChController = GetComponent<CharacterController>();
            m_PlayerInput = GetComponent<PlayerInput>();
            m_Animator = GetComponent<Animator>();
            m_MainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            ComputeMovement();
        }

        private void ComputeMovement()
        {
            Vector3 moveInput = m_PlayerInput.MoveInput.normalized;
            m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            m_ForwardSpeed = Mathf.MoveTowards(
                m_ForwardSpeed,
                m_DesiredForwardSpeed,
                Time.fixedDeltaTime);


            m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
        }
    }
}
