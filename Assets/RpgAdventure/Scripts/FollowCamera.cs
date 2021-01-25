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

        Debug.Log("CURRENT: " + currentRotationAngle);
        Debug.Log("WANTED: " + wantedRotationAngle);

        // a + (b - a) * t
        // c + (w - c) * t
        // 1 -> 67.5 + (90 - 67.5) * 0.5 = 67.5 + 11.25 = 78.75
        // 2 -> 78.75 + (90 - 78.75) * 0.5 = 78.75 + 5.625 = 84.375

        // 0 -> Current, a
        // 1 -> Wanted, b
        currentRotationAngle = Mathf.LerpAngle(
            currentRotationAngle,
            wantedRotationAngle,
            0.5f);

        Debug.Log("FINAL: " + currentRotationAngle);

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
