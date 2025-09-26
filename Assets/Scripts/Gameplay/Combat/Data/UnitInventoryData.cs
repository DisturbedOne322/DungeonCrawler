using System.Collections.Generic;
using Gameplay.Combat.Consumables;
using ModestTree;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitInventoryData
    {
        public ReactiveCollection<BaseConsumable> Items { get; } = new();

        public void AssignItems(List<BaseConsumable> items) => Items.AllocFreeAddRange(items);
        public void AddItem(BaseConsumable consumable) => Items.Add(consumable);
        public void RemoveItem(BaseConsumable consumable) => Items.Remove(consumable);
    }
}