
using JustGame.Script.Items;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.World
{
    public class WoodPile : PickableItem
    {
        [SerializeField] private Item m_item;


        public override void Pick()
        {
            InventoryManager.Instance.AddItem(Item.CreateItem(m_item));
            base.Pick();
        }
    }  
}

