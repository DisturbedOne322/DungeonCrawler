using System;
using Gameplay.Combat;
using Gameplay.Combat.Skills;

namespace UI.PlayerBattleMenu
{
    public class MenuItemData
    {
        public string Label { get; }
        public Func<bool> IsSelectable { get; }
        public Action OnSelected { get; }

        public MenuItemData(string label, Func<bool> isSelectable, Action onSelected)
        {
            Label = label;
            IsSelectable = isSelectable;
            OnSelected = onSelected;
        }

        public static MenuItemData ForSkill(BaseSkill skill, 
            CombatService service, 
            Action<BaseSkill> onSkillSelected)
        {
            return new MenuItemData(
                skill.Name,
                () => skill.CanUse(service),
                () => onSkillSelected(skill)
            );
        }

        public static MenuItemData Simple(string label, Action onSelected)
        {
            return new MenuItemData(label, () => true, onSelected);
        }
    }
}