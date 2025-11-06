using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Dungeon.Rooms.SkillRoom;
using UnityEngine;

namespace Installers.ServiceInstallers
{
    public class PhysicalMasterRoomInstaller : SinglePurchaseRoomInstaller
    {
        [SerializeField] private PhysicalSkillsMasterConfig _physicalSkillsMasterConfig;

        protected override BaseSellableItemsConfig ItemsConfig => _physicalSkillsMasterConfig;
    }
}