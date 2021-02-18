using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemPrefab;
        public LayerMask targetLayers;

        void Start()
        {
            Instantiate(itemPrefab, transform);
            Destroy(transform.GetChild(0).gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (0 != (targetLayers.value & 1 << other.gameObject.layer))
            {
                Debug.Log("Adding Item");
                Destroy(gameObject);
            }
        }
    }
}