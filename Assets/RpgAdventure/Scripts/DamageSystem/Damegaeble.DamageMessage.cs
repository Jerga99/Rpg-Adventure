using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public partial class Damageable : MonoBehaviour
    {
        public struct DamageMessage
        {
            public MonoBehaviour damager;
            public int amount;
            public Vector3 damageSource;
        }
    }
}
