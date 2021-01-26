using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        float currentRotationAngle = transform.eulerAngles.y;
        float wantedRotationAngle = target.eulerAngles.y;

        currentRotationAngle = Mathf.LerpAngle(
            currentRotationAngle,
            wantedRotationAngle,
            0.5f);

        transform.position = new Vector3(
            target.position.x,
            5.0f,
            target.position.z);

        // currentRotationAngle degreed rotation around Y axis
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // rotate vector forward currentRotationAngle angle degrees around Y axis
        Vector3 rotatedPosition = currentRotation * Vector3.forward;

        transform.position -= rotatedPosition * 10;

        transform.LookAt(target);
    }
}
