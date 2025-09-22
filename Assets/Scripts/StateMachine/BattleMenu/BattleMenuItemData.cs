using System;
using Gameplay.Combat;
using Gameplay.Combat.Skills;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class BattleMenuItemData
    {
        public string Label { get; }
        public string Description { get; }
        public Func<bool> IsSelectable { get; }
        public Action OnSelected { get; }
        
        public ReactiveProperty<bool> IsHighlighted { get; } = new ReactiveProperty<bool>();

        public BattleMenuItemData(string label, Func<bool> isSelectable, Action onSelected, string description = null)
        {
            Label = label;
            IsSelectable = isSelectable;
            OnSelected = onSelected;
            Description = description;
        }

        public static BattleMenuItemData ForSkill(BaseSkill skill, 
            CombatService service, 
            Action onSkillSelected)
        {
            return new BattleMenuItemData(
                skill.Name,
                () => skill.CanUse(service),
                onSkillSelected,
                skill.Description
            );
        }

        public static BattleMenuItemData Simple(string label, Action onSelected) => new(label, () => true, onSelected);
    }
}