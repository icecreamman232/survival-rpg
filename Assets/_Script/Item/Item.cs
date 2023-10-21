using UnityEngine;

namespace JustGame.Script.Items
{
    public enum ItemCategory
    {
        RAW,
        REFINED,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Item")]
    public class Item : ScriptableObject
    {
        public ItemCategory Category;
        public string Name;
        public bool IsEdible;
        public int Amount;

        public static Item CreateItem(Item source)
        {
            var newItem = ScriptableObject.CreateInstance<Item>();
            newItem.Category = source.Category;
            newItem.Name = source.Name;
            newItem.IsEdible = source.IsEdible;
            newItem.Amount = source.Amount;
            return newItem;
        }
    }
}

