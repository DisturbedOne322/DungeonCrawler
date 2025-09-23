using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Units;
using UniRx;

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

        public override void LoadMenuItems()
        {
            MenuItems.Clear();
            
            MenuItems.Add(
                BattleMenuItemData.ForSkill(
                    Player.UnitSkillsData.BasicAttackSkill,
                    CombatService,
                    () => StateMachine.SelectSkill(Player.UnitSkillsData.BasicAttackSkill))
                );
            
            MenuItems.Add(
                BattleMenuItemData.ForSkill(
                    Player.UnitSkillsData.GuardSkill,
                    CombatService,
                    () => StateMachine.SelectSkill(Player.UnitSkillsData.GuardSkill))
            );
            
            MenuItems.Add(
                BattleMenuItemData.Simple(
                    "SKILLS", 
                    () => StateMachine.GoToState<SkillSelectBattleMenuState>().Forget())
            );
            
            MenuItemsUpdater.SetMenuItems(MenuItems);
        }

        protected override void SubscribeToInputEvents()
        {
            _disposables = new();
            
            _disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));
            _disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            _disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
        }

        protected override void UnsubscribeFromInputEvents()
        {
            _disposables.Dispose();
        }
    }
}