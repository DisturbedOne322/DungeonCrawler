using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.Rooms.SkillRoom;
using UnityEngine;

namespace Installers.ServiceInstallers
{
    public class PhysicalMasterRoomInstaller : SinglePurchaseRoomInstaller
    {
        [SerializeField] private PhysicalSkillsMasterConfig _physicalSkillsMasterConfig;

        protected override BasePurchasableItemsConfig ItemsConfig => _physicalSkillsMasterConfig;
    }
}