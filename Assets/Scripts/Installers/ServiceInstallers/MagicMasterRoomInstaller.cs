using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Dungeon.Rooms.SkillRoom;
using UnityEngine;

namespace Installers.ServiceInstallers
{
    public class MagicMasterRoomInstaller : SinglePurchaseRoomInstaller
    {
        [SerializeField] private MagicSkillsMasterConfig _magicSkillsMasterConfig;

        protected override BaseSellableItemsConfig ItemsConfig => _magicSkillsMasterConfig;
    }
}