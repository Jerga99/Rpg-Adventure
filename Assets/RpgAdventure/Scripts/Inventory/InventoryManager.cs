using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RpgAdventure
{
    public class InventoryManager : MonoBehaviour
    {
        public List<InventorySlot> inventory = new List<InventorySlot>();
        public Transform invetoryPanel;

        private int m_InventorySize;

        private void Awake()
        {
            m_InventorySize = invetoryPanel.childCount;
            CreateInventory(m_InventorySize);
        }

        private void CreateInventory(int size)
        {
            for (int i = 0; i < size; i++)
            {
                inventory.Add(new InventorySlot(i));
            }
        }

        public void OnItemPickup(GameObject item)
        {
            AddItem(item);
        }

        private void AddItem(GameObject item)
        {
            Debug.Log(inventory.Count);
        }
    }
}
