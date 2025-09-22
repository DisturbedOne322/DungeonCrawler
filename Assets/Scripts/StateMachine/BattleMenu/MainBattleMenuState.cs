using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Units;
using UniRx;
using UnityEngine;

namespace StateMachine.BattleMenu
{
    public class MainBattleMenuState : BattleMenuState
    {
        private CompositeDisposable _disposables;
        
        public MainBattleMenuState(PlayerUnit player, 
            PlayerInputProvider playerInputProvider, 
            MenuItemsUpdater menuItemsUpdater, 
            CombatService combatService) : 
            base(player, 
                playerInputProvider, 
                menuItemsUpdater, 
                combatService)
        {
            
        }

        protected override void LoadMenuItems()
        {
            MenuItems.Clear();
            
            MenuItems.Add(
                BattleMenuItem.ForSkill(
                    Player.UnitSkillsData.BasicAttackSkill,
                    CombatService,
                    () => StateMachine.SelectSkill(Player.UnitSkillsData.BasicAttackSkill))
                );
            
            MenuItems.Add(
                BattleMenuItem.ForSkill(
                    Player.UnitSkillsData.GuardSkill,
                    CombatService,
                    () => StateMachine.SelectSkill(Player.UnitSkillsData.GuardSkill))
            );
            
            MenuItems.Add(
                BattleMenuItem.Simple(
                    "SKILLS", 
                    () => StateMachine.GoToState<SkillSelectBattleMenuState>().Forget())
            );
        }

        protected override void SubscribeToInputEvents()
        {
            _disposables = new();
            
            _disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection(MenuItems)));
            _disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(MenuItems, -1)));
            _disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(MenuItems, +1)));
        }

        protected override void UnsubscribeFromInputEvents()
        {
            _disposables.Dispose();
        }
    }
}