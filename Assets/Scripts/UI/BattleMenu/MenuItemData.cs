using System;
using Gameplay.Combat.Consumables;
using Gameplay.Combat.Skills;
using UniRx;

namespace UI.BattleMenu
{
    public class MenuItemData
    {
        public string Label { get; }
        public string Description { get; }
        public Func<bool> IsSelectable { get; }
        public Action OnSelected { get; }
        public int Quantity { get; }
        public MenuItemType Type { get; }

        public ReactiveProperty<bool> IsHighlighted { get; } = new ReactiveProperty<bool>();

        public MenuItemData(
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

        public static MenuItemData ForSkill(
            BaseSkill skill,
            Func<bool> isSelectable,
            Action onSkillSelected)
        {
            return new MenuItemData(
                skill.Name,
                isSelectable,
                onSkillSelected,
                MenuItemType.Skill,
                skill.Description
            );
        }

        public static MenuItemData ForItem(
            BaseConsumable consumable,
            Func<bool> isSelectable,
            Action onItemSelected,
            int quantity)
        {
            return new MenuItemData(
                consumable.Name,
                isSelectable,
                onItemSelected,
                MenuItemType.Item,
                consumable.Description,
                quantity
            );
        }

        public static MenuItemData ForSubmenu(
            string label,
            Func<bool> isSelectable,
            Action onSelected,
            string description = null)
        {
            return new MenuItemData(label, 
                isSelectable, 
                onSelected, 
                MenuItemType.Submenu,
                description);
        }
    }
}