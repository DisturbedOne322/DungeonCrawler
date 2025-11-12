using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.Rooms.SkillRoom;
using UnityEngine;

namespace Installers.ServiceInstallers
{
    public class MagicMasterRoomInstaller : SinglePurchaseRoomInstaller
    {
        [SerializeField] private MagicSkillsMasterConfig _magicSkillsMasterConfig;

        protected override BasePurchasableItemsConfig ItemsConfig => _magicSkillsMasterConfig;
    }
}