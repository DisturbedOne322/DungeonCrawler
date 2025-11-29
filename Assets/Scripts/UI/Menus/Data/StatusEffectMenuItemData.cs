using System;
using Gameplay;

namespace UI.Menus.Data
{
    public class StatusEffectMenuItemData : MenuItemData
    {
        private readonly BaseGameItem _statusEffect;
        public BaseGameItem StatusEffect => _statusEffect;
        
        public StatusEffectMenuItemData(BaseGameItem statusEffect, 
            Func<bool> selectableFunc, 
            Action onSelected) : 
            base(statusEffect.Name, 
                selectableFunc, 
                onSelected, 
                MenuItemType.StatusEffect,
                statusEffect.Description)
        {
            _statusEffect = statusEffect;
        }
    }
}