using RpgAdventure;
using UnityEngine;

[System.Serializable]
public class PlayerScanner
{
    public float meleeDetectionRadius = 2.0f;
    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    public PlayerController Detect(Transform detector)
    {

        if (PlayerController.Instance == null)
        {
            return null;
        }

        Vector3 toPlayer = PlayerController.Instance.transform.position - detector.position;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            if ((Vector3.Dot(toPlayer.normalized, detector.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad)) ||
                toPlayer.magnitude <= meleeDetectionRadius)
            {
                return PlayerController.Instance;
            }
        }

        return null;
    }
}
