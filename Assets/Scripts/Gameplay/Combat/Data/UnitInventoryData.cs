using System.Collections.Generic;
using Gameplay.Combat.Items;

namespace Gameplay.Combat.Data
{
    public class UnitInventoryData
    {
        public List<BaseItem> Items { get; } = new();

        public void AssignItems(List<BaseItem> items) => Items.AddRange(items);
        public void AddItem(BaseItem item) => Items.Add(item);
        public void RemoveItem(BaseItem item) => Items.Remove(item);
    }
}