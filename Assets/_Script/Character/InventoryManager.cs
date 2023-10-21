using System.Collections.Generic;
using JustGame.Script.Items;
using JustGame.Scripts.Managers;
using UnityEngine;


namespace JustGame.Script.Managers
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        [SerializeField] private List<Item> m_inventory;

        private void Start()
        {
            m_inventory = new List<Item>();
        }
        
        /// <summary>
        /// Add this item to inventory. If this item's exist, will increase its amount
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            if (HasItem(item, out var index))
            {
                AddItemAmount(index,item.Amount );
            }
            else
            {
                m_inventory.Add(item);
            }
        }

        /// <summary>
        /// Add amount to this item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        public void AddItemAmount(int index, int amount)
        {
            m_inventory[index].Amount += amount;
        }
        
        /// <summary>
        /// Check if this item's exist. If true return its index in inventory.
        /// Return -1 if it's not found.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public bool HasItem(Item item, out int itemIndex)
        {
            for (int i = 0; i < m_inventory.Count; i++)
            {
                if (m_inventory[i].Category == item.Category)
                {
                    if (m_inventory[i].Name == item.Name)
                    {
                        itemIndex = i;
                        return true;
                    }
                }
            }

            itemIndex = -1;
            return false;
        }
        /// <summary>
        /// Remove item at index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            if (m_inventory[index] == null) return;
            Destroy(m_inventory[index]);
            m_inventory[index] = null;
        }
    }
}

