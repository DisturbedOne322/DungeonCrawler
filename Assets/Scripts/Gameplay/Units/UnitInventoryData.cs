using System.Collections.Generic;
using Gameplay.Consumables;
using ModestTree;
using UniRx;

namespace Gameplay.Units
{
    public class UnitInventoryData
    {
        public readonly ReactiveProperty<int> Coins = new();
        public ReactiveCollection<BaseConsumable> Consumables { get; } = new();
        
        public void AddItems(List<BaseConsumable> items)
        {
            Consumables.AllocFreeAddRange(items);
        }

        public void AddItems(BaseConsumable item, int amount)
        {
            for (var i = 0; i < amount; i++)
                Consumables.Add(item);
        }

        public void AddItem(BaseConsumable consumable)
        {
            Consumables.Add(consumable);
        }

        public void RemoveItem(BaseConsumable consumable)
        {
            Consumables.Remove(consumable);
        }
    }
}