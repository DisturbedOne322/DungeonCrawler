using System.Collections.Generic;
using Gameplay.Combat.Items;
using ModestTree;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitInventoryData
    {
        public ReactiveCollection<BaseItem> Items { get; } = new();

        public void AssignItems(List<BaseItem> items) => Items.AllocFreeAddRange(items);
        public void AddItem(BaseItem item) => Items.Add(item);
        public void RemoveItem(BaseItem item) => Items.Remove(item);
    }
}