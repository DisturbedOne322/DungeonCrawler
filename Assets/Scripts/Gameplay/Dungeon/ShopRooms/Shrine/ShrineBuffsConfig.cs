using System.Collections.Generic;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.Shrine
{
    [CreateAssetMenu(fileName = "ShrineBuffsConfig", menuName = "Gameplay/Shop/Shrine Buffs Config")]
    public class ShrineBuffsConfig : BasePurchasableItemsConfig
    {
        [SerializeField] private List<PurchasableItemData<BaseStatusEffectData>> _statusEffectItems;

        public override IReadOnlyList<IPurchasableItemData> ItemsForSale => _statusEffectItems;
    }
}