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

        public override void LoadMenuItems()
        {
            MenuItems.Clear();

            var skillsData = Player.UnitSkillsData;

            foreach (var skill in skillsData.Skills)
            {
                MenuItems.Add(
                    BattleMenuItemData.ForSkill(
                        skill,
                        CombatService,
                        () => StateMachine.SelectSkill(skill))
                );
            }
            
            MenuItemsUpdater.SetMenuItems(MenuItems);
        }

        protected override void SubscribeToInputEvents()
        {
            _disposables = new();
            
            _disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));            
            _disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            _disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
            _disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ => StateMachine.GoToPrevState().Forget()));
        }

        protected override void UnsubscribeFromInputEvents()
        {
            _disposables.Dispose();
        }
    }
}