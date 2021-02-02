using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;
    private float m_SpeedModifier = 0.7f;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void OnAnimatorMove()
    {
        if (m_NavMeshAgent == null)
        {
            return;
        }

        m_NavMeshAgent.speed =
            (m_Animator.deltaPosition / Time.fixedDeltaTime).magnitude * m_SpeedModifier;
    }

    public bool SetFollowTarget(Vector3 position)
    {
        return m_NavMeshAgent.SetDestination(position);
    }
}
