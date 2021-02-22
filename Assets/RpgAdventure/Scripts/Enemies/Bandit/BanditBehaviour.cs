using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RpgAdventure
{
    public class BanditBehaviour : MonoBehaviour, IMessageReceiver, IAttackAnimListener
    {
        public PlayerScanner playerScanner;
        public MeleeWeapon meleeWeapon;
        public float timeToStopPursuit = 2.0f;
        public float timeToWaitOnPursuit = 2.0f;
        public float attackDistance = 1.1f;

        public bool HasFollowTarget
        {
            get
            {
                return m_FollowTarget != null;
            }
        }

        private PlayerController m_FollowTarget;
        private EnemyController m_EnemyController;

        private float m_TimeSinceLostTarget = 0;
        private Vector3 m_OriginPosition;
        private Quaternion m_OriginRotation;

        private readonly int m_HashInPursuit = Animator.StringToHash("InPursuit");
        private readonly int m_HashNearBase = Animator.StringToHash("NearBase");
        private readonly int m_HashAttack = Animator.StringToHash("Attack");
        private readonly int m_HashHurt = Animator.StringToHash("Hurt");
        private readonly int m_HashDead = Animator.StringToHash("Dead");

        private void Awake()
        {
            m_EnemyController = GetComponent<EnemyController>();
            m_OriginPosition = transform.position;
            m_OriginRotation = transform.rotation;
            meleeWeapon.SetOwner(gameObject);
            meleeWeapon.SetTargetLayer(1 << PlayerController.Instance.gameObject.layer);
        }

        private void Update()
        {
            if (PlayerController.Instance.IsRespawning)
            {
                GoToOriginalSpot();
                CheckIfNearBase();
                return;
            }

            GuardPosition();
        }

        private void GuardPosition()
        {
            var detectedTarget = playerScanner.Detect(transform);
            bool hasDetectedTarget = detectedTarget != null;

            if (hasDetectedTarget) { m_FollowTarget = detectedTarget; }

            if (HasFollowTarget)
            {
                AttackOrFollowTarget();

                if (hasDetectedTarget)
                {
                    m_TimeSinceLostTarget = 0;
                }
                else
                {
                    StopPursuit();
                }
            }

            CheckIfNearBase();
        }

        public void OnReceiveMessage(MessageType type, object sender, object msg)
        {
            switch (type)
            {
                case MessageType.DEAD:
                    OnDead();
                    break;
                case MessageType.DAMAGED:
                    OnReceiveDamage();
                    break;
                default:
                    break;
            }
        }

        public void MeleeAttackStart()
        {
            meleeWeapon.BeginAttack();
        }

        public void MeleeAttackEnd()
        {
            meleeWeapon.EndAttack();
        }

        private void OnDead()
        {
            m_EnemyController.StopFollowTarget();
            m_EnemyController.Animator.SetTrigger(m_HashDead);
        }

        private void OnReceiveDamage()
        {
            m_EnemyController.Animator.SetTrigger(m_HashHurt);
        }

        private void GoToOriginalSpot()
        {
            m_FollowTarget = null;
            m_EnemyController.Animator.SetBool(m_HashInPursuit, false);
            m_EnemyController.FollowTarget(m_OriginPosition);
        }

        private void StopPursuit()
        {
            m_TimeSinceLostTarget += Time.deltaTime;

            if (m_TimeSinceLostTarget >= timeToStopPursuit)
            {
                m_FollowTarget = null;
                m_EnemyController.Animator.SetBool(m_HashInPursuit, false);
                StartCoroutine(WaitBeforeReturn());
            }
        }

        private void AttackOrFollowTarget()
        {
            Vector3 toTarget = m_FollowTarget.transform.position - transform.position;
            if (toTarget.magnitude <= attackDistance)
            {
                AttackTarget(toTarget);
            }
            else
            {
                FollowTarget();
            }
        }

        private void AttackTarget(Vector3 toTarget)
        {
            var toTargetRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toTargetRotation,
                360 * Time.deltaTime);

            m_EnemyController.StopFollowTarget();
            m_EnemyController.Animator.SetTrigger(m_HashAttack);
        }

        private void FollowTarget()
        {
            m_EnemyController.Animator.SetBool(m_HashInPursuit, true);
            m_EnemyController.FollowTarget(m_FollowTarget.transform.position);
        }

        private void CheckIfNearBase()
        {
            Vector3 toBase = m_OriginPosition - transform.position;
            toBase.y = 0;

            bool nearBase = toBase.magnitude < 0.01f;
            m_EnemyController.Animator.SetBool(m_HashNearBase, nearBase);

            if (nearBase)
            {
                Quaternion targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    m_OriginRotation,
                    360 * Time.deltaTime);

                transform.rotation = targetRotation;
            }
        }

        private IEnumerator WaitBeforeReturn()
        {
            yield return new WaitForSeconds(timeToWaitOnPursuit);
            m_EnemyController.FollowTarget(m_OriginPosition);
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Color c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedForward = Quaternion.Euler(
                0,
                -playerScanner.detectionAngle * 0.5f,
                0) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                Vector3.up,
                rotatedForward,
                playerScanner.detectionAngle,
                playerScanner.detectionRadius);


            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                Vector3.up,
                rotatedForward,
                360,
                playerScanner.meleeDetectionRadius);

        }
#endif

    }
}
