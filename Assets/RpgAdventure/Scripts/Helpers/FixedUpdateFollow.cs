using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class FixedUpdateFollow : MonoBehaviour
    {
        public Transform toFollow;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (toFollow == null) { return; }
            transform.position = toFollow.position;
            transform.rotation = toFollow.rotation;
        }

        public void SetFolowee(Transform folowee)
        {
            toFollow = folowee;
        }
    }
}
