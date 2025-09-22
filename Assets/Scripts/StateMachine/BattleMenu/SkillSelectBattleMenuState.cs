using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Units;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class SkillSelectBattleMenuState : BattleMenuState
    {
        private CompositeDisposable _disposables;
            
        public SkillSelectBattleMenuState(PlayerUnit player, 
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

            var skillsData = Player.UnitSkillsData;

            foreach (var skill in skillsData.Skills)
            {
                MenuItems.Add(
                    BattleMenuItem.ForSkill(
                        skill,
                        CombatService,
                        () => StateMachine.SelectSkill(skill))
                );
            }
        }

        protected override void SubscribeToInputEvents()
        {
            _disposables = new();
            
            _disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ =>
            {
                MenuItemsUpdater.ExecuteSelection(MenuItems);
                StateMachine.Reset();
            }));            
            
            _disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(MenuItems, -1)));
            _disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(MenuItems, +1)));
            _disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ => StateMachine.GoToState<MainBattleMenuState>().Forget()));
        }

        protected override void UnsubscribeFromInputEvents()
        {
            _disposables.Dispose();
        }
    }
}