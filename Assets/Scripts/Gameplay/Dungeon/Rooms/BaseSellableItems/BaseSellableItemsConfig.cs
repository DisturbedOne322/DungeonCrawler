using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public abstract class BaseSellableItemsConfig : ScriptableObject
    {
        public abstract IReadOnlyList<ISellableItemData> ItemsForSale { get; }
    }
}