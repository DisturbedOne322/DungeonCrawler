using System;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.BasePurchasableItems
{
    [Serializable]
    public class PurchasableItemData<T> : IPurchasableItemData where T : BaseGameItem
    {
        [SerializeField] private T _item;

        [SerializeField] [Min(0)] private int _price;
        [SerializeField] [Min(1)] private int _amount = 1;

        public BaseGameItem Item => _item;
        public int Price => _price;
        public int Amount => _amount;
    }
}