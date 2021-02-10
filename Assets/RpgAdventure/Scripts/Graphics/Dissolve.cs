using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class Dissolve : MonoBehaviour
    {
        public float dissolveTime = 6.0f;

        private void Awake()
        {
            dissolveTime += Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time >= dissolveTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
