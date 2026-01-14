using System;
using Gameplay;

namespace UI.Menus.Data
{
    public class StatusEffectMenuItemData : MenuItemData
    {
        public StatusEffectMenuItemData(BaseGameItem statusEffect,
            Func<bool> selectableFunc,
            Action onSelected) :
            base(statusEffect.Name,
                selectableFunc,
                onSelected,
                MenuItemType.StatusEffect,
                statusEffect.Description)
        {
            StatusEffect = statusEffect;
        }

        public BaseGameItem StatusEffect { get; }
    }
}