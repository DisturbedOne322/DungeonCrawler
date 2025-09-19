using Data;
using Gameplay.Dungeon.Areas;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class DungeonFactory : MonoBehaviour
    {
        [SerializeField] private CorridorArea _corridorPrefab;
        [SerializeField] private DecisionArea _decisionPrefab;

        private ContainerFactory _containerFactory;

        [Inject]
        private void Construct(ContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
        }
        
        public DungeonArea CreateArea(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.Corridor:
                    return _containerFactory.Create<CorridorArea>(_corridorPrefab.gameObject);
                case RoomType.Decision:
                    return _containerFactory.Create<DecisionArea>(_decisionPrefab.gameObject);
            }
            
            return null;
        }
    }
}