using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public partial class Damageable : MonoBehaviour
    {
        public int maxHitPoints;
        [Range(0, 360.0f)]
        public float hitAngle = 360.0f;

        public void ApplyDamage(DamageMessage data)
        {
            Debug.Log(data.amount);
            Debug.Log(data.damager);
            Debug.Log(data.damageSource);

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
