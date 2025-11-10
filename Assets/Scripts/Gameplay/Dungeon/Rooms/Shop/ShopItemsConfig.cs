using System.Collections.Generic;
using Gameplay.Consumables;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Equipment;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.Shop
{
    [CreateAssetMenu(fileName = "ShopItemsConfig", menuName = "Gameplay/Shop/Shop Items Config")]
    public class ShopItemsConfig : ScriptableObject
    {
        [SerializeField] private List<SellableItemData<BaseConsumable>> _consumableItems;
        [SerializeField] private List<SellableItemData<BaseEquipmentPiece>> _equipmentItems;
        [SerializeField] private List<SellableItemData<BaseSkill>> _skillItems;
        [SerializeField] private List<SellableItemData<BaseStatusEffectData>> _statusEffectItems;

        public IReadOnlyList<ISellableItemData> ConsumableItems => _consumableItems;
        public IReadOnlyList<ISellableItemData> EquipmentItems => _equipmentItems;
        public IReadOnlyList<ISellableItemData> SkillItems => _skillItems;
        public IReadOnlyList<ISellableItemData> StatusEffectItems => _statusEffectItems;
    }
}