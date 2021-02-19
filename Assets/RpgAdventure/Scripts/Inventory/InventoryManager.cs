using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RpgAdventure
{
    public class InventoryManager : MonoBehaviour
    {
        public Dictionary<string, GameObject> inventory = new Dictionary<string, GameObject>();
        public int inventorySize;

        public void AddItem(GameObject item)
        {
            if (!inventory.ContainsKey(item.name))
            {
                inventory.Add(item.name, item);
                Debug.Log(inventory.Count);
                Debug.Log(inventory.ContainsKey(item.name));
            }
        }
    }
}
