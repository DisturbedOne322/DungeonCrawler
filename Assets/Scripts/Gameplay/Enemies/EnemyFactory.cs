using AssetManagement.AssetProviders.Core;
using Gameplay.Progression;
using Gameplay.Services;
using Gameplay.Units;

namespace Gameplay.Enemies
{
    public class EnemyFactory
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;
        private readonly ContainerFactory _containerFactory;

        private EnemyFactory(BaseConfigProvider<GameplayConfig> configProvider, ContainerFactory containerFactory)
        {
            _configProvider = configProvider;
            _containerFactory = containerFactory;
        }
        
        public EnemyUnit CreateEnemy()
        {
            var enemyDatabase = _configProvider.GetConfig<EnemyDatabase>();
            
            var data = enemyDatabase.Database[0];
            var prefab = data.Prefab.gameObject;
            var unit = _containerFactory.Create<EnemyUnit>(prefab);
            
            unit.InitializeUnit(data);
            unit.SetExperienceBonus(data.ExperienceBonus);
            
            return unit;
        }
    }
}