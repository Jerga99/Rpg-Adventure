using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

        public void OnItemPickup(ItemSpawner spawner)
        {
            AddItemFrom(spawner);
        }

        private void AddItemFrom(ItemSpawner spawner)
        {
            var inventorySlot = GetFreeSlot();
            if (inventorySlot == null) { return; }

            var item = spawner.itemPrefab;
            inventorySlot.Place(item);
            invetoryPanel
                .GetChild(inventorySlot.index)
                .GetComponentInChildren<Text>().text = item.name;

            Destroy(spawner.gameObject);

        }

        private InventorySlot GetFreeSlot()
        {
            return inventory.Find(slot => slot.itemName == null);
        }

    }
}
