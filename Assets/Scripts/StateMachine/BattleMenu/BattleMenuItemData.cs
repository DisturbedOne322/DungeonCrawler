using System;
using Gameplay.Combat;
using Gameplay.Combat.Items;
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
        public int Quantity { get; }
        
        public ReactiveProperty<bool> IsHighlighted { get; } = new ReactiveProperty<bool>();

        public BattleMenuItemData(string label, Func<bool> isSelectable, Action onSelected, string description = null, int quantity = 1)
        {
            Label = label;
            IsSelectable = isSelectable;
            OnSelected = onSelected;
            Description = description;
            Quantity = quantity;
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
        
        public static BattleMenuItemData ForItem(BaseItem item, 
            CombatService service, 
            Action onSkillSelected,
            int quantity)
        {
            return new BattleMenuItemData(
                item.Name,
                () => item.CanUse(service),
                onSkillSelected,
                item.Description,
                quantity
            );
        }

        public static BattleMenuItemData Simple(string label, Action onSelected) => new(label, () => true, onSelected);
    }
}