using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using UI.BattleMenu;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class MainBattleMenuState : BattleMenuState
    {
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

        public override void InitMenuItems()
        {
            MenuItems.Add(
                MenuItemData.ForSkillItem(
                    Player.UnitSkillsData.BasicAttackSkill,
                    () => true,
                    () => StateMachine.SelectAction(Player.UnitSkillsData.BasicAttackSkill))
            );

            MenuItems.Add(
                MenuItemData.ForSkillItem(
                    Player.UnitSkillsData.GuardSkill,
                    () => true,
                    () => StateMachine.SelectAction(Player.UnitSkillsData.GuardSkill))
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "SKILLS",
                    () => Player.UnitSkillsData.Skills.Count > 0,
                    () => StateMachine.GoToState<SkillSelectBattleMenuState>().Forget())
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "ITEMS",
                    () => Player.UnitInventoryData.Consumables.Count > 0,
                    () => StateMachine.GoToState<ItemSelectBattleMenuState>().Forget())
            );
        }

        protected override void SubscribeToInputEvents()
        {
            Disposables = new CompositeDisposable();

            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
        }
    }
}