using UnityEngine;

namespace RpgAdventure
{
    public class BanditBehaviour : MonoBehaviour
    {
        public float detectionRadius = 10.0f;
        public float detectionAngle = 90.0f;

        private void Update()
        {
            LookForPlayer();
        }

        private PlayerController LookForPlayer() {

            if (PlayerController.Instance == null)
            {
                return null;
            }

            Vector3 enemyPosition = transform.position;
            Vector3 toPlayer = PlayerController.Instance.transform.position - enemyPosition;
            toPlayer.y = 0;

            if (toPlayer.magnitude <= detectionRadius)
            {
                Debug.Log("Detecting the player!");
            } else
            {
                Debug.Log("Where are you?");
            }

            return null;
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Color c = new Color(0.8f, 0, 0, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedForward = Quaternion.Euler(
                0,
                -detectionAngle * 0.5f,
                0) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                Vector3.up,
                rotatedForward,
                detectionAngle,
                detectionRadius);

        }
#endif

    }
}
