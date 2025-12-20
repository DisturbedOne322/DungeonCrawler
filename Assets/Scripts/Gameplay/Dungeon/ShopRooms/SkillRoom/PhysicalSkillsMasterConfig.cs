using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.SkillRoom
{
    [CreateAssetMenu(fileName = "PhysicalSkillsMasterConfig", menuName = "Gameplay/Shop/Physical Skills Master Config")]
    public class PhysicalSkillsMasterConfig : BasePurchasableItemsConfig
    {
        [SerializeField] private List<PurchasableItemData<BaseSkill>> _skillsForSale;

        public override IReadOnlyList<IPurchasableItemData> ItemsForSale => _skillsForSale;
    }
}