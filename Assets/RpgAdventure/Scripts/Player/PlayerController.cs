using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://assetstore.unity.com/packages/3d/characters/medieval-cartoon-warriors-90079

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        const float k_Acceleration = 20.0f;
        const float k_Deceleration = 35.0f;

        public float maxForwardSpeed = 8.0f;
        public float rotationSpeed;

        private PlayerInput m_PlayerInput;
        private CharacterController m_ChController;
        private Animator m_Animator;
        private CameraController m_CameraController;

        private Quaternion m_TargetRotation;

        private float m_DesiredForwardSpeed;
        private float m_ForwardSpeed;

        private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");

        private void Awake()    
        {
            m_ChController = GetComponent<CharacterController>();
            m_PlayerInput = GetComponent<PlayerInput>();
            m_Animator = GetComponent<Animator>();
            m_CameraController = GetComponent<CameraController>();
        }

        private void FixedUpdate()
        {
            ComputeMovement();
            ComputeRotation();

            if (m_PlayerInput.IsMoveInput)
            {
                transform.rotation = m_TargetRotation;
            }
        }
 
        private void ComputeMovement()
        {
            Vector3 moveInput = m_PlayerInput.MoveInput.normalized;
            m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            float acceleration = m_PlayerInput.IsMoveInput ? k_Acceleration : k_Deceleration;

            m_ForwardSpeed = Mathf.MoveTowards(
                m_ForwardSpeed,
                m_DesiredForwardSpeed,
                Time.fixedDeltaTime * acceleration);


            m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
        }

        private void ComputeRotation()
        {
            Vector3 moveInput = m_PlayerInput.MoveInput.normalized;

            if (moveInput.x >= 0.7 && moveInput.z >= 0.7)
            {
                Debug.Log("Moving");
            }

            Vector3 cameraDirection = Quaternion.Euler(
                0,
                m_CameraController.freeLookCamera.m_XAxis.Value,
                0) * Vector3.forward;

            Quaternion movementRotation = Quaternion.FromToRotation(Vector3.forward, moveInput);
            Quaternion targetRotation = Quaternion.LookRotation(movementRotation * cameraDirection);

            m_TargetRotation = targetRotation;
        }
    }
}
