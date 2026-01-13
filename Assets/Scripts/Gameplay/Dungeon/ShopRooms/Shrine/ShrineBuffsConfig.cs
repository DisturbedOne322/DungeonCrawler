using System.Collections.Generic;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.Shrine
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayShop + "Shrine Buffs Config")]
    public class ShrineBuffsConfig : BasePurchasableItemsConfig
    {
        [SerializeField] private List<PurchasableItemData<BaseStatusEffectData>> _statusEffectItems;

        public override IReadOnlyList<IPurchasableItemData> ItemsForSale => _statusEffectItems;
    }
}