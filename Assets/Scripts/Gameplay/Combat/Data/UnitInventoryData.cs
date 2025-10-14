using System.Collections.Generic;
using Gameplay.Consumables;
using ModestTree;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitInventoryData
    {
        public ReactiveCollection<BaseConsumable> Consumables { get; } = new();
        public ReactiveProperty<int> Coins = new();

        public void AddItems(List<BaseConsumable> items) => Consumables.AllocFreeAddRange(items);
        public void AddItems(BaseConsumable item, int amount)
        {
            for (int i = 0; i < amount; i++) 
                Consumables.Add(item);
        }

        public void AddItem(BaseConsumable consumable) => Consumables.Add(consumable);
        public void RemoveItem(BaseConsumable consumable) => Consumables.Remove(consumable);
    }
}