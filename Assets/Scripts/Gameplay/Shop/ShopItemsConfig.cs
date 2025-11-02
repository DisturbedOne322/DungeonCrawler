using System.Collections.Generic;
using Gameplay.Consumables;
using Gameplay.Equipment;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Shop
{
    [CreateAssetMenu(fileName = "ShopItemsConfig", menuName = "Gameplay/Shop/Shop Items Config")]
    public class ShopItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ShopItemData<BaseConsumable>> _consumableItems;
        [SerializeField] private List<ShopItemData<BaseEquipmentPiece>> _equipmentItems;
        [SerializeField] private List<ShopItemData<BaseSkill>> _skillItems;
        [SerializeField] private List<ShopItemData<BaseStatusEffectData>> _statusEffectItems;
        
        public IReadOnlyList<ShopItemData<BaseConsumable>> ConsumableItems => _consumableItems;
        public IReadOnlyList<ShopItemData<BaseEquipmentPiece>> EquipmentItems => _equipmentItems;
        public IReadOnlyList<ShopItemData<BaseSkill>> SkillItems => _skillItems;
        public IReadOnlyList<ShopItemData<BaseStatusEffectData>> StatusEffectItems => _statusEffectItems;
    }
}