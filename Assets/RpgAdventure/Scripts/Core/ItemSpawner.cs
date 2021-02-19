using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace RpgAdventure
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemPrefab;
        public LayerMask targetLayers;
        public UnityEvent<GameObject> onItemPickup;

        void Start()
        {
            Instantiate(itemPrefab, transform);
            Destroy(transform.GetChild(0).gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (0 != (targetLayers.value & 1 << other.gameObject.layer))
            {
                onItemPickup.Invoke(itemPrefab);
                Destroy(gameObject);
            }
        }
    }
}