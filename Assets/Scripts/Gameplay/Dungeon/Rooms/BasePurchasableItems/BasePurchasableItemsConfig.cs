using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.BasePurchasableItems
{
    public abstract class BasePurchasableItemsConfig : ScriptableObject
    {
        public abstract IReadOnlyList<IPurchasableItemData> ItemsForSale { get; }
    }
}