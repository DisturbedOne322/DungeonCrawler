using System;
using Gameplay.Combat;
using Gameplay.Combat.Skills;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class BattleMenuItem
    {
        public string Label { get; }
        public Func<bool> IsSelectable { get; }
        public Action OnSelected { get; }
        
        public ReactiveProperty<bool> IsHighlighted { get; } = new ReactiveProperty<bool>();

        public BattleMenuItem(string label, Func<bool> isSelectable, Action onSelected)
        {
            Label = label;
            IsSelectable = isSelectable;
            OnSelected = onSelected;
        }

        public static BattleMenuItem ForSkill(BaseSkill skill, 
            CombatService service, 
            Action onSkillSelected)
        {
            return new BattleMenuItem(
                skill.Name,
                () => skill.CanUse(service),
                onSkillSelected
            );
        }

        public static BattleMenuItem Simple(string label, Action onSelected) => new(label, () => true, onSelected);
    }
}