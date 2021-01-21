using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private Vector3 m_Offset;

    void Start()
    {
        m_Offset = transform.position - target.transform.position;  
    }

    void LateUpdate()
    {
        Vector3 newPosition = target.transform.position + m_Offset;
        Vector3 newRotation = new Vector3(
            transform.eulerAngles.x,
            target.transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        transform.position = newPosition;
        transform.eulerAngles = newRotation;
    }
}
