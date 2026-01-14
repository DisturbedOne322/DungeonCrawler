using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus.Data;

namespace UI.InventoryDisplay.Skills
{
    public class SkillsHeldMenu : BaseInventoryDisplayMenu
    {
        public SkillsHeldMenu(PlayerUnit player, PlayerInputProvider inputProvider) : base(player, inputProvider)
        {
        }

        protected override void BuildItems(List<MenuItemData> target)
        {
            var skills = Player.UnitSkillsData.Skills;
            skills.Insert(0, Player.UnitSkillsData.GuardSkill);
            skills.Insert(0, Player.UnitSkillsData.BasicAttackSkill);

            foreach (var skill in skills)
                target.Add(MenuItemData.ForSkillItem(skill, () => true, () => { }));
        }
    }
}