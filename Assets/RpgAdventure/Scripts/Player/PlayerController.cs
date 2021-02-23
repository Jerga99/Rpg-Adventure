using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://assetstore.unity.com/packages/3d/characters/medieval-cartoon-warriors-90079

namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour, IAttackAnimListener, IMessageReceiver
    {
        public static PlayerController Instance
        {
            get
            {
                return s_Instance;
            }
        }

        public bool IsRespawning { get { return m_IsRespawning; } }

        public MeleeWeapon meleeWeapon;
        public float maxForwardSpeed = 8.0f;
        public float rotationSpeed;
        public float m_MaxRotationSpeed = 1200;
        public float m_MinRotationSpeed = 800;
        public float gravity = 20.0f;
        public Transform attackHand;
        public RandomAudioPlayer sprintAudio;

        private static PlayerController s_Instance;
        private PlayerInput m_PlayerInput;
        private Damageable m_Damageable;
        private CharacterController m_ChController;
        private Animator m_Animator;
        private CameraController m_CameraController;
        private HudManager m_HudManager;
        private Quaternion m_TargetRotation;

        private AnimatorStateInfo m_CurrentStateInfo;
        private AnimatorStateInfo m_NextStateInfo;
        private bool m_IsAnimatorTransitioning;
        private bool m_IsRespawning;

        private float m_DesiredForwardSpeed;
        private float m_ForwardSpeed;
        private float m_VerticalSpeed;

        // Animator Parameter Hashes
        private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private readonly int m_HashMeleeAttack = Animator.StringToHash("MeleeAttack");
        private readonly int m_HashDeath = Animator.StringToHash("Death");
        private readonly int m_HashFootFall = Animator.StringToHash("FootFall");

        // Animator Tag Hashes
        private readonly int m_HashBlockInput = Animator.StringToHash("BlockInput");

        const float k_Acceleration = 20.0f;
        const float k_Deceleration = 35.0f;

        private void Awake()
        {
            m_ChController = GetComponent<CharacterController>();
            m_PlayerInput = GetComponent<PlayerInput>();
            m_Animator = GetComponent<Animator>();
            m_Damageable = GetComponent<Damageable>();
            m_CameraController = Camera.main.GetComponent<CameraController>();
            m_HudManager = FindObjectOfType<HudManager>();
            s_Instance = this;

            m_HudManager.SetMaxHealth(m_Damageable.maxHitPoints);
        }

        private void FixedUpdate()
        {
            CacheAnimationState();
            UpdateInputBlocking();
            ComputeForwardMovement();
            ComputeVerticalMovement();
            ComputeRotation();

            if (m_PlayerInput.IsMoveInput)
            {
                float rotationSpeed = Mathf.Lerp(m_MaxRotationSpeed, m_MinRotationSpeed, m_ForwardSpeed / m_DesiredForwardSpeed);
                m_TargetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    m_TargetRotation,
                    rotationSpeed * Time.fixedDeltaTime);

                transform.rotation = m_TargetRotation;
            }

            m_Animator.ResetTrigger(m_HashMeleeAttack);
            if (m_PlayerInput.IsAttack)
            {
                m_Animator.SetTrigger(m_HashMeleeAttack);
            }

            PlaySprintAudio();
        }

        private void OnAnimatorMove()
        {
            if (m_IsRespawning) { return; }

            Vector3 movement = m_Animator.deltaPosition;
            movement += m_VerticalSpeed * Vector3.up * Time.fixedDeltaTime;
            m_ChController.Move(movement);
        }

        public void OnReceiveMessage(MessageType type, object sender, object msg)
        {
            if (type == MessageType.DAMAGED)
            {
                m_HudManager.SetHealth((sender as Damageable).CurrentHitPoints);
            }

            if (type == MessageType.DEAD)
            {
                m_IsRespawning = true;
                m_Animator.SetTrigger(m_HashDeath);
                m_HudManager.SetHealth(0);
            }
        }

        public void MeleeAttackStart()
        {
            if (meleeWeapon != null)
            {
                meleeWeapon.BeginAttack();
            }
        }

        public void MeleeAttackEnd()
        {
            if (meleeWeapon != null)
            {
                meleeWeapon.EndAttack();
            }
        }

        public void StartRespawn()
        {
            transform.position = Vector3.zero;
            m_HudManager.SetHealth(m_Damageable.maxHitPoints);
            m_Damageable.SetInitialHealth();
        }

        public void FinishRespawn()
        {
            m_IsRespawning = false;
        }

        public void UseItemFrom(InventorySlot slot)
        {
            if (meleeWeapon != null)
            {
                if (slot.itemPrefab.name == meleeWeapon.name) { return; }
                else
                {
                    Destroy(meleeWeapon.gameObject);
                }
            }

            meleeWeapon = Instantiate(slot.itemPrefab, transform)
                .GetComponent<MeleeWeapon>();
            meleeWeapon.GetComponent<FixedUpdateFollow>().SetFolowee(attackHand);
            meleeWeapon.name = slot.itemPrefab.name;
            meleeWeapon.SetOwner(gameObject);
        }

        private void ComputeVerticalMovement()
        {
            m_VerticalSpeed = -gravity;
        }

        private void ComputeForwardMovement()
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
            Vector3 cameraDirection = Quaternion.Euler(
                0,
                m_CameraController.PlayerCam.m_XAxis.Value,
                0) * Vector3.forward;

            Quaternion targetRotation;

            if (Mathf.Approximately(Vector3.Dot(moveInput, Vector3.forward), -1.0f))
            {
                targetRotation = Quaternion.LookRotation(-cameraDirection);
            }
            else
            {
                Quaternion movementRotation = Quaternion.FromToRotation(Vector3.forward, moveInput);
                targetRotation = Quaternion.LookRotation(movementRotation * cameraDirection);
            }

            m_TargetRotation = targetRotation;
        }

        private void CacheAnimationState()
        {
            m_CurrentStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            m_NextStateInfo = m_Animator.GetNextAnimatorStateInfo(0);
            m_IsAnimatorTransitioning = m_Animator.IsInTransition(0);
        }

        private void UpdateInputBlocking()
        {
            bool inputBlocked = m_CurrentStateInfo.tagHash == m_HashBlockInput && !m_IsAnimatorTransitioning;
            inputBlocked |= m_NextStateInfo.tagHash == m_HashBlockInput;
            m_PlayerInput.isPlayerControllerInputBlocked = inputBlocked;
        }

        private void PlaySprintAudio()
        {
            float footFallCurve = m_Animator.GetFloat(m_HashFootFall);

            if (footFallCurve > 0.01f && !sprintAudio.isPlaying && sprintAudio.canPlay)
            {
                sprintAudio.isPlaying = true;
                sprintAudio.canPlay = false;
                sprintAudio.PlayRandomClip();
            }
            else if (sprintAudio.isPlaying)
            {
                sprintAudio.isPlaying = false;
            }
            else if (footFallCurve < 0.01f && !sprintAudio.canPlay)
            {
                sprintAudio.canPlay = true;
            }
        }
    }
}
