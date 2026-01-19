using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using UI.Menus;
using UI.Menus.Data;

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
            AddSkillMenuItem(Player.UnitSkillsContainer.BasicAttackSkill);
            AddSkillMenuItem(Player.UnitSkillsContainer.GuardSkill);

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "SKILLS",
                    () => Player.UnitSkillsContainer.HeldSkills.Count > 0,
                    () => StateMachine.GoToState<SkillSelectBattleMenuState>().Forget())
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "ITEMS",
                    () => Player.UnitInventoryData.Consumables.Count > 0,
                    () => StateMachine.GoToState<ItemSelectBattleMenuState>().Forget())
            );
        }

        private void AddSkillMenuItem(SkillHeldData skillHeldData)
        {
            var skill = skillHeldData.Skill;
            MenuItems.Add(
                MenuItemData.ForSkillItem(
                    skill,
                    () => skillHeldData.CanUse(CombatService),
                    () =>
                    {
                        skillHeldData.SetCooldown();
                        StateMachine.SelectAction(skill);
                    })
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