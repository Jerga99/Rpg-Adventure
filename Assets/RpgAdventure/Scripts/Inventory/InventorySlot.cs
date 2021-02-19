using System;
using UnityEngine;

namespace RpgAdventure
{
    [System.Serializable]
    public class InventorySlot
    {
        public int index;
        public string itemName;
        public GameObject itemPrefab;

        public InventorySlot(int index)
        {
            this.index = index;
        }
    }

}

