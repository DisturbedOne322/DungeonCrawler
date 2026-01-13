using System.Collections.Generic;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.SkillRoom
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayShop + "Physical Skills Master Config")]
    public class PhysicalSkillsMasterConfig : BasePurchasableItemsConfig
    {
        [SerializeField] private List<PurchasableItemData<BaseSkill>> _skillsForSale;

        public override IReadOnlyList<IPurchasableItemData> ItemsForSale => _skillsForSale;
    }
}