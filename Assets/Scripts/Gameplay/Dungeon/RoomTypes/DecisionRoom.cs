using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Animations;
using Gameplay.Dungeon.Data;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.RoomTypes
{
    [RequireComponent(typeof(DoorAnimator))]
    public class DecisionRoom : StopRoom
    {
        [SerializeField] private DoorAnimator _doorAnimator;
        [SerializeField] private List<DecisionDoor> _doors;

        private DungeonBranchingController _dungeonBranchingController;
        private DungeonBranchingSelector _dungeonBranchingSelector;
        private DungeonVisualsConfigProvider _dungeonVisualsConfigProvider;
        private GameplayConfigsProvider _gameplayConfigProvider;

        public override RoomType RoomType => RoomType.Decision;

        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController,
            DungeonBranchingSelector dungeonBranchingSelector,
            GameplayConfigsProvider gameplayConfigsProvider,
            DungeonVisualsConfigProvider dungeonVisualsConfigProvider)
        {
            _dungeonBranchingController = dungeonBranchingController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _gameplayConfigProvider = gameplayConfigsProvider;
            _dungeonVisualsConfigProvider = dungeonVisualsConfigProvider;
        }

        public override void ResetRoom()
        {
            _doorAnimator.ResetDoors();
        }

        public override void SetupRoom()
        {
            var roomsDatabase = _gameplayConfigProvider.GetConfig<DungeonRoomsDatabase>();
            var visualsDatabase = _dungeonVisualsConfigProvider.GetConfig();

            var doorTypes = _dungeonBranchingSelector.RoomsForSelection;
            for (var i = 0; i < doorTypes.Count; i++)
            {
                var door = _doors[i];

                if (!roomsDatabase.TryGetRoom(doorTypes[i], out var data))
                    continue;

                if (!visualsDatabase.TryGetRoomIcon(data.RoomType, out var icon))
                    continue;

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