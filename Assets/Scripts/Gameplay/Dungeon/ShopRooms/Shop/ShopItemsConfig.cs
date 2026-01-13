using System.Collections.Generic;
using Data.Constants;
using Gameplay.Consumables;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Equipment;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.Shop
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayShop + "Shop Items Config")]
    public class ShopItemsConfig : ScriptableObject
    {
        [SerializeField] private List<PurchasableItemData<BaseConsumable>> _consumableItems;
        [SerializeField] private List<PurchasableItemData<BaseEquipmentPiece>> _equipmentItems;
        [SerializeField] private List<PurchasableItemData<BaseSkill>> _skillItems;
        [SerializeField] private List<PurchasableItemData<BaseStatusEffectData>> _statusEffectItems;

        public IReadOnlyList<IPurchasableItemData> ConsumableItems => _consumableItems;
        public IReadOnlyList<IPurchasableItemData> EquipmentItems => _equipmentItems;
        public IReadOnlyList<IPurchasableItemData> SkillItems => _skillItems;
        public IReadOnlyList<IPurchasableItemData> StatusEffectItems => _statusEffectItems;
    }
}