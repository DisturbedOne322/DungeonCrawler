using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Animations;
using Gameplay.Dungeon.RoomVariants;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(DoorAnimator))]
    public class DecisionRoom : VariantRoom<DecisionRoomVariantData>
    {
        [SerializeField] private DoorAnimator _doorAnimator;
        [SerializeField] private List<DecisionDoor> _doors;

        private DungeonBranchingController _dungeonBranchingController;
        private DungeonBranchingSelector _dungeonBranchingSelector;
        private DungeonVisualsConfigProvider _dungeonVisualsConfigProvider;

        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController,
            DungeonBranchingSelector dungeonBranchingSelector,
            DungeonVisualsConfigProvider dungeonVisualsConfigProvider)
        {
            _dungeonBranchingController = dungeonBranchingController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _dungeonVisualsConfigProvider = dungeonVisualsConfigProvider;
        }

        public override void ResetRoom()
        {
            _doorAnimator.ResetDoors();
        }

        public override void SetupRoom()
        {
            var visualsDatabase = _dungeonVisualsConfigProvider.GetConfig();

            var doorTypes = _dungeonBranchingSelector.RoomsForSelection;
            for (var i = 0; i < doorTypes.Count; i++)
            {
                var door = _doors[i];

                if (!visualsDatabase.TryGetRoomIcon(doorTypes[i].RoomType, out var icon))
                {
                    Debug.LogError($"Could not find room icon for {doorTypes[i].RoomType}");
                    continue;
                }

                door.SetDoorIcon(icon);
            }
        }

        public override async UniTask ClearRoom()
        {
            var doorIndex = await _dungeonBranchingController.WaitForDecision();
            await _doorAnimator.PlayOpenAnimation(doorIndex);
        }
    }
}