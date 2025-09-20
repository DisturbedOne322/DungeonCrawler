using Gameplay.Combat;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyDatabase _enemyDatabase;

        private ContainerFactory _containerFactory;

        [Inject]
        private void Construct(ContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
        }
        
        public EnemyUnit CreateEnemy()
        {
            var data = _enemyDatabase.Database[0];
            var prefab = data.Prefab.gameObject;
            var unit = _containerFactory.Create<EnemyUnit>(prefab);
            
            unit.InitializeUnit(data);
            
            return unit;
        }
    }
}