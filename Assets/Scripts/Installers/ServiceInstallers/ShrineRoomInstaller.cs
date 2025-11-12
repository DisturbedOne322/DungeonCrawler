using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.Rooms.Shrine;
using UnityEngine;

namespace Installers.ServiceInstallers
{
    public class ShrineRoomInstaller : SinglePurchaseRoomInstaller
    {
        [SerializeField] private ShrineBuffsConfig _shrineBuffsConfig;

        protected override BasePurchasableItemsConfig ItemsConfig => _shrineBuffsConfig;
    }
}