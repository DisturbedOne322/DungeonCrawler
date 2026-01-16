using System.Collections.Generic;
using Gameplay;
using Gameplay.Player;
using Gameplay.Skills.Core;
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
            var skills = new List<BaseSkill>(Player.UnitSkillsContainer.Skills);
            skills.Insert(0, Player.UnitSkillsContainer.GuardSkill);
            skills.Insert(0, Player.UnitSkillsContainer.BasicAttackSkill);

            foreach (var skill in skills)
                target.Add(MenuItemData.ForSkillItem(skill, () => true, () => { }));
        }
    }
}