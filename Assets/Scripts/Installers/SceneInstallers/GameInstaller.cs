using Data;
using Gameplay;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUnit _prefab;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerUnit>().FromComponentInNewPrefab(_prefab).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
            
            Container.Bind<GameSequenceController>().AsSingle();
            Container.Bind<CombatSequenceController>().AsSingle();
            
            Container.Bind<GameplayData>().AsSingle();
            Container.Bind<BalanceService>().AsSingle();
            
            Container.Bind<ContainerFactory>().AsSingle();
            Container.Bind<EnemyFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerFactory>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerMovementHistory>().AsSingle();
            Container.Bind<PlayerDecisionProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputProvider>().AsSingle();
        }
    }
}