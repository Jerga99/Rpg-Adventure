using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RpgAdventure
{
    public class BanditBehaviour : MonoBehaviour
    {
        public PlayerScanner playerScanner;
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

        private Animator m_Animator;
        private float m_TimeSinceLostTarget = 0;
        private Vector3 m_OriginPosition;
        private Quaternion m_OriginRotation;

        private readonly int m_HashInPursuit = Animator.StringToHash("InPursuit");
        private readonly int m_HashNearBase = Animator.StringToHash("NearBase");
        private readonly int m_HashAttack = Animator.StringToHash("Attack");

        private void Awake()
        {
            m_EnemyController = GetComponent<EnemyController>();
            m_Animator = GetComponent<Animator>();

            m_OriginPosition = transform.position;
            m_OriginRotation = transform.rotation;
        }

        private void Update()
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

        private void StopPursuit()
        {
            m_TimeSinceLostTarget += Time.deltaTime;

            if (m_TimeSinceLostTarget >= timeToStopPursuit)
            {
                m_FollowTarget = null;
                m_Animator.SetBool(m_HashInPursuit, false);
                StartCoroutine(WaitOnPursuit());
            }
        }

        private void AttackOrFollowTarget()
        {
            Vector3 toTarget = m_FollowTarget.transform.position - transform.position;
            if (toTarget.magnitude <= attackDistance)
            {
                m_EnemyController.StopFollowTarget();
                m_Animator.SetTrigger(m_HashAttack);
            }
            else
            {
                m_Animator.SetBool(m_HashInPursuit, true);
                m_EnemyController.FollowTarget(m_FollowTarget.transform.position);
            }
        }

        private void CheckIfNearBase()
        {
            Vector3 toBase = m_OriginPosition - transform.position;
            toBase.y = 0;

            bool nearBase = toBase.magnitude < 0.01f;
            m_Animator.SetBool(m_HashNearBase, nearBase);

            if (nearBase)
            {
                Quaternion targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    m_OriginRotation,
                    360 * Time.deltaTime);

                transform.rotation = targetRotation;
            }
        }

        private IEnumerator WaitOnPursuit()
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

        }
#endif

    }
}
