using System;
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

        public void AddItem(Item item)
        {
            m_inventory.Add(item);
        }
    }
}

