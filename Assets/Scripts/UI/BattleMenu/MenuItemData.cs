using System;
using Gameplay;
using UI.Menus;
using UniRx;

namespace UI.BattleMenu
{
    public class MenuItemData
    {
        public string Label { get; }
        public string Description { get; }

        public Action OnSelected { get; }
        public MenuItemType Type { get; }
        
        public int OriginalQuantity { get; }
        
        public Func<bool> SelectableFunc { get; }

        public readonly BoolReactiveProperty IsSelectable = new();
        public readonly BoolReactiveProperty IsHighlighted = new();
        
        public MenuItemData(
            string label,
            Func<bool> selectableFunc,
            Action onSelected,
            MenuItemType type,
            string description = null,
            int amount = 1)
        {
            Label = label;
            OnSelected = onSelected;
            Type = type;
            Description = description;
            
            SelectableFunc = selectableFunc;
            OriginalQuantity = amount;
        }
        
        public static MenuItemData ForSkillItem(
            BaseGameItem gameItem,
            Func<bool> selectablePredicate,
            Action onItemSelected,
            int quantity = 1)
        {
            return new MenuItemData(
                gameItem.Name,
                selectablePredicate,
                onItemSelected,
                MenuItemType.Skill,
                gameItem.Description,
                quantity
            );
        }
        
        public static MenuItemData ForConsumableItem(
            BaseGameItem gameItem,
            Func<bool> selectablePredicate,
            Action onItemSelected,
            int quantity = 1)
        {
            return new MenuItemData(
                gameItem.Name,
                selectablePredicate,
                onItemSelected,
                MenuItemType.Consumable,
                gameItem.Description,
                quantity
            );
        }
        
        public static MenuItemData ForStatusEffectItem(
            BaseGameItem gameItem,
            Func<bool> selectablePredicate,
            Action onItemSelected,
            int quantity = 1)
        {
            return new MenuItemData(
                gameItem.Name,
                selectablePredicate,
                onItemSelected,
                MenuItemType.StatusEffect,
                gameItem.Description,
                quantity
            );
        }

        public static MenuItemData ForSubmenu(
            string label,
            Func<bool> selectablePredicate,
            Action onSelected,
            string description = "")
        {
            return new MenuItemData(label,
                selectablePredicate,
                onSelected,
                MenuItemType.Submenu,
                description);
        }
        
        public static MenuItemData ForNavigationItem(
            string label,
            Action onSelected)
        {
            var data = new MenuItemData(label,
                () => true,
                onSelected,
                MenuItemType.Submenu,
                "")
            {
                IsSelectable =
                {
                    Value = true
                }
            };

            return data;
        }
    }
}