using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Animations;
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
        
        private RoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController,
            DungeonBranchingSelector dungeonBranchingSelector,
            DungeonVisualsConfigProvider dungeonVisualsConfigProvider)
        {
            _dungeonBranchingController = dungeonBranchingController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _dungeonVisualsConfigProvider = dungeonVisualsConfigProvider;
        }
        
        public void SetData(RoomVariantData data) => _roomData = data;

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