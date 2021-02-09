using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public partial class Damageable : MonoBehaviour
    {
        [Range(0, 360.0f)]
        public float hitAngle = 360.0f;
        public int maxHitPoints;
        public int CurrentHitPoints { get; private set; }
        public List<MonoBehaviour> onDamageMessageReceivers;

        public enum MessageType
        {
            DAMAGED,
            DEAD
        }

        private void Awake()
        {
            CurrentHitPoints = maxHitPoints;
        }

        public void ApplyDamage(DamageMessage data)
        {
            if (CurrentHitPoints <= 0)
            {
                return;
            }

            Vector3 positionToDamager = data.damageSource - transform.position;
            positionToDamager.y = 0;

            if (Vector3.Angle(transform.forward, positionToDamager) > hitAngle * 0.5f)
            {
                return;
            }

            CurrentHitPoints -= data.amount;

            var messageType =
                CurrentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

            for (int i = 0; i < onDamageMessageReceivers.Count; i++)
            {
                var receiver = onDamageMessageReceivers[i];
                Debug.Log(messageType);
                Debug.Log(receiver);
            }

        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = new Color(0.0f, 0.0f, 1.0f, 0.5f);

            Vector3 rotatedForward =
                Quaternion.AngleAxis(-hitAngle * 0.5f, transform.up) * transform.forward;

            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                transform.up,
                rotatedForward,
                hitAngle,
                1.0f);
        }
#endif
    }
}
