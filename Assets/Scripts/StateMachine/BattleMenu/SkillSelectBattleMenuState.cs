using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus;
using UI.Menus.Data;

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
            var skillsData = Player.UnitSkillsContainer;

            foreach (var skill in skillsData.Skills)
                MenuItems.Add(
                    MenuItemData.ForSkillItem(
                        skill,
                        () => skill.CanUse(CombatService),
                        () => StateMachine.SelectAction(skill))
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

        public override void OnUIBack()
        {
            StateMachine.GoToPrevState().Forget();
        }
    }
}