using System.Collections.Generic;
using Gameplay.Player;
using UniRx;

namespace UI.PlayerBattleMenu
{
    public class SkillsMenuState : BaseMenuState
    {
        private CompositeDisposable _disposables;
        
        public SkillsMenuState(MenuItemsUpdater menuItemsUpdater, PlayerInputProvider playerInputProvider, BattleMenuController battleMenuController) : 
            base(menuItemsUpdater, playerInputProvider, battleMenuController)
        {
            
        }
        
        public override void EnterState()
        {
            _disposables = new();
            
            _disposables.Add(
                PlayerInputProvider.OnUiUp.Subscribe(_ => 
                    MenuItemsUpdater.UpdateItems(MenuItems, ref SelectedItemIndex, -1)
                ));
            
            _disposables.Add(
                PlayerInputProvider.OnUiDown.Subscribe(_ => 
                    MenuItemsUpdater.UpdateItems(MenuItems, ref SelectedItemIndex, +1)
                ));
            
            _disposables.Add(
                PlayerInputProvider.OnUiBack.Subscribe(_ => 
                    BattleMenuController.ReturnToMainMenu()
                ));
            
            _disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ =>
            {
                MenuItems[SelectedItemIndex].Data.OnSelected?.Invoke();
            }));
            
            MenuItemsUpdater.UpdateItems(MenuItems, ref SelectedItemIndex);
        }

        public override void ExitState()
        {
            _disposables?.Dispose();
        }
    }
}