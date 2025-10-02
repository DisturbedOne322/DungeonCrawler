using System.Collections.Generic;
using AssetManagement.AssetProviders.Core;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Animations;
using Gameplay.Dungeon.Data;
using Gameplay.Progression;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(DoorAnimator))]
    public class DecisionRoom : StopRoom
    {
        [SerializeField] private DoorAnimator _doorAnimator;
        [SerializeField] private List<DecisionDoor> _doors;

        private DungeonBranchingController _dungeonBranchingController;
        private BaseConfigProvider<GameplayConfig> _configProvider;
        private DungeonBranchingSelector _dungeonBranchingSelector;

        public override RoomType RoomType => RoomType.Decision;
        
        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController, 
            BaseConfigProvider<GameplayConfig> configProvider,
            DungeonBranchingSelector dungeonBranchingSelector)
        {
            _dungeonBranchingController = dungeonBranchingController;
            _configProvider = configProvider;
            _dungeonBranchingSelector = dungeonBranchingSelector;
        }

        public override void ResetRoom()
        {
            _doorAnimator.ResetDoors();
        }

        public override void SetupRoom()
        {
            var doorTypes = _dungeonBranchingSelector.RoomsForSelection;
            for (int i = 0; i < doorTypes.Count; i++)
            {
                var door = _doors[i];
                var config = _configProvider.GetConfig<DungeonRoomsDatabase>();
                
                if(config.TryGetRoomData(doorTypes[i], out var data)) 
                    door.SetDoorIcon(data.Icon);
            } 
        }

        public override async UniTask ClearRoom()
        {
            int doorIndex =  await _dungeonBranchingController.WaitForDecision();
            await _doorAnimator.PlayOpenAnimation(doorIndex);
        }
    }
}