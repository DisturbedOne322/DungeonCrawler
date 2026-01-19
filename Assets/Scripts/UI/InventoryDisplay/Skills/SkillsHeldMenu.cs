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
            var skills = new List<BaseSkill>(Player.UnitSkillsContainer.HeldSkills.Count + 2);

            foreach (var heldSkill in Player.UnitSkillsContainer.HeldSkills) 
                skills.Add(heldSkill.Skill);
            
            skills.Insert(0, Player.UnitSkillsContainer.GuardSkill.Skill);
            skills.Insert(0, Player.UnitSkillsContainer.BasicAttackSkill.Skill);

            foreach (var skill in skills)
                target.Add(MenuItemData.ForSkillItem(skill, () => true, () => { }));
        }
    }
}