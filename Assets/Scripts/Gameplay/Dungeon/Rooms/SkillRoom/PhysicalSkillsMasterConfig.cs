using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms.SkillRoom
{
    [CreateAssetMenu(fileName = "PhysicalSkillsMasterConfig", menuName = "Gameplay/Shop/Physical Skills Master Config")]
    public class PhysicalSkillsMasterConfig : BaseSellableItemsConfig
    {
        [SerializeField] private List<SellableItemData<BaseSkill>> _skillsForSale;

        public override IReadOnlyList<ISellableItemData> ItemsForSale => _skillsForSale;
    }
}