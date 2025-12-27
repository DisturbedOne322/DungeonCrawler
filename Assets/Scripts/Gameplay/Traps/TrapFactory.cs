using System.Collections.Generic;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Services;
using UnityEngine;

namespace Gameplay.Traps
{
    public class TrapFactory
    {
        private readonly ContainerFactory _containerFactory;
        
        public TrapFactory(ContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
        }

        public TrapMono CreateTrap(TrapRoomVariantData roomData)
        {
            var trapPrefabs = roomData.TrapPrefabs;

            List<TrapMono> selection = new();

            foreach (var prefab in trapPrefabs)
            {
                if(!prefab.TryGetComponent(out TrapMono trap))
                    continue;
                
                selection.Add(trap);
            }
            
            var index = Random.Range(0, selection.Count);
            var selectedPrefab =  selection[index];
            var instance = _containerFactory.Create(selectedPrefab);
            return instance;
        }
    }
}