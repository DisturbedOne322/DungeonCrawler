using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using UI.BattleMenu;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class SkillSelectBattleMenuState : BattleMenuState
    {
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

        public override void InitMenuItems()
        {
            var skillsData = Player.UnitSkillsData;

            foreach (var skill in skillsData.Skills)
                MenuItems.Add(
                    MenuItemData.ForGameItem(
                        skill,
                        () => skill.CanUse(CombatService),
                        () => StateMachine.SelectAction(skill))
                );
        }

        protected override void SubscribeToInputEvents()
        {
            Disposables = new CompositeDisposable();

            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
            Disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ => StateMachine.GoToPrevState().Forget()));
        }
    }
}