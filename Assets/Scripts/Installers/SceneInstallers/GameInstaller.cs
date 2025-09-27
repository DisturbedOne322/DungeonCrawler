using Data;
using Gameplay;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Installers.SceneInstallers
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
            Container.Bind<CombatService>().AsSingle();
            Container.Bind<CombatFormulaService>().AsSingle();
            Container.Bind<CombatBuffsApplicator>().AsSingle();
            Container.Bind<ModifiersCalculationService>().AsSingle();
            
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerMovementHistory>().AsSingle();
            Container.Bind<PlayerDecisionProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputProvider>().AsSingle();

            Container.Bind<RewardSelectorService>().AsSingle();
        }
    }
}