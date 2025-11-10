using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.Shrine
{
    [CreateAssetMenu(fileName = "ShrineBuffsConfig", menuName = "Gameplay/Shop/Shrine Buffs Config")]
    public class ShrineBuffsConfig : BaseSellableItemsConfig
    {
        [SerializeField] private List<SellableItemData<BaseStatusEffectData>> _statusEffectItems;

        public override IReadOnlyList<ISellableItemData> ItemsForSale => _statusEffectItems;
    }
}