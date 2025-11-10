using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using UI.BattleMenu;

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

        public override void OnUISubmit()
        {
            MenuItemsUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            MenuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            MenuItemsUpdater.UpdateSelection(+1);
        }
    }
}