using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.ConfigProviders;
using Gameplay.Services;
using Gameplay.Units;

namespace Gameplay.Enemies
{
    public class EnemyFactory
    {
        private readonly ContainerFactory _containerFactory;
        private readonly EnemyDatabase _enemyDatabase;

        private EnemyFactory(GameplayConfigsProvider configProvider, ContainerFactory containerFactory)
        {
            _enemyDatabase = configProvider.GetConfig<EnemyDatabase>();
            _containerFactory = containerFactory;
        }

        public EnemyUnit CreateEnemy()
        {
            var data = _enemyDatabase.Database[0];
            var prefab = data.Prefab.gameObject;
            var unit = _containerFactory.Create<EnemyUnit>(prefab);

            unit.InitializeUnit(data);
            unit.SetExperienceBonus(data.ExperienceBonus);

            return unit;
        }
    }
}