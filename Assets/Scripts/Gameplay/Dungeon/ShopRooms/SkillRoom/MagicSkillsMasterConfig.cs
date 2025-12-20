using System.Collections.Generic;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Dungeon.ShopRooms.SkillRoom
{
    [CreateAssetMenu(fileName = "MagicSkillsMasterConfig", menuName = "Gameplay/Shop/Magic Skills Master Config")]
    public class MagicSkillsMasterConfig : BasePurchasableItemsConfig
    {
        [SerializeField] private List<PurchasableItemData<BaseSkill>> _skillsForSale;
        public override IReadOnlyList<IPurchasableItemData> ItemsForSale => _skillsForSale;
    }
}