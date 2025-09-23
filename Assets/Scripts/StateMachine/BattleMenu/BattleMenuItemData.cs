using System;
using Gameplay.Combat;
using Gameplay.Combat.Items;
using Gameplay.Combat.Skills;
using UI.BattleMenu;
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
        public MenuItemType Type { get; }

        public ReactiveProperty<bool> IsHighlighted { get; } = new ReactiveProperty<bool>();

        public BattleMenuItemData(
            string label,
            Func<bool> isSelectable,
            Action onSelected,
            MenuItemType type,
            string description = null,
            int quantity = 1)
        {
            Label = label;
            IsSelectable = isSelectable;
            OnSelected = onSelected;
            Type = type;
            Description = description;
            Quantity = quantity;
        }

        public static BattleMenuItemData ForSkill(
            BaseSkill skill,
            CombatService service,
            Action onSkillSelected)
        {
            return new BattleMenuItemData(
                skill.Name,
                () => skill.CanUse(service),
                onSkillSelected,
                MenuItemType.Skill,
                skill.Description
            );
        }

        public static BattleMenuItemData ForItem(
            BaseItem item,
            CombatService service,
            Action onItemSelected,
            int quantity)
        {
            return new BattleMenuItemData(
                item.Name,
                () => item.CanUse(service),
                onItemSelected,
                MenuItemType.Item,
                item.Description,
                quantity
            );
        }

        public static BattleMenuItemData ForSubmenu(
            string label,
            Func<bool> isSelectable,
            Action onSelected,
            string description = null)
        {
            return new BattleMenuItemData(label, 
                isSelectable, 
                onSelected, 
                MenuItemType.Submenu,
                description);
        }
    }
}