using Bootstrap;
using Data;
using Gameplay;
using Gameplay.Buffs;
using Gameplay.Buffs.Services;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using Gameplay.Experience;
using Gameplay.Player;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Installers.GameInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUnit _prefab;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerUnit>().FromComponentInNewPrefab(_prefab).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameplayBootstrapper>().AsSingle();
            
            Container.Bind<PlayerInitializer>().AsSingle();
            
            BindCombat();

            BindProgression();

            BindMovement();
        }

        private void BindCombat()
        {
            Container.Bind<GameSequenceController>().AsSingle();
            Container.Bind<CombatSequenceController>().AsSingle();
            Container.Bind<CombatService>().AsSingle();
            Container.Bind<CombatEventsBus>().AsSingle();
            Container.Bind<CombatFormulaService>().AsSingle();
            Container.Bind<CombatBuffsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CombatBuffsApplicationService>().AsSingle().NonLazy();
            Container.Bind<BuffsCalculationService>().AsSingle();
        }

        private void BindProgression()
        {
            Container.Bind<PlayerExperienceService>().AsSingle();
            Container.Bind<ExperienceRequirementsProvider>().AsSingle();
            Container.Bind<ExperienceData>().AsSingle();
            Container.Bind<PlayerStatDistributionController>().AsSingle();
            Container.Bind<PlayerStatDistributionTableBuilder>().AsSingle();
            Container.Bind<PlayerStatDistrubutionController>().AsSingle();
            Container.Bind<PlayerSkillSlotsManager>().AsSingle();
            Container.Bind<PlayerLevelUpBonusApplier>().AsSingle();
            Container.Bind<PlayerLevelUpController>().AsSingle();
        }

        private void BindMovement()
        {
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerMovementHistory>().AsSingle();
            Container.Bind<PlayerDecisionProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputProvider>().AsSingle();
        }
    }
}