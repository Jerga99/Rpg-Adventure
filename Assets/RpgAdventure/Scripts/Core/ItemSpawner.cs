using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemPrefab;

        void Start()
        {
            Instantiate(itemPrefab, transform);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

}