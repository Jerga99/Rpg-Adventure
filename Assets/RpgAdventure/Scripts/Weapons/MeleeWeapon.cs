using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class MeleeWeapon : MonoBehaviour
    {
        public int damage = 10;

        public void BeginAttack()
        {
            Debug.Log("Weapon is swinging!");
        }
    }
}
