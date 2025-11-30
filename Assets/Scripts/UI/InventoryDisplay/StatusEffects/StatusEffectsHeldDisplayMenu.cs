using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus.Data;

namespace UI.InventoryDisplay.StatusEffects
{
    public class StatusEffectsHeldDisplayMenu : BaseInventoryDisplayMenu
    {
        public StatusEffectsHeldDisplayMenu(PlayerUnit player, 
            PlayerInputProvider inputProvider) : base(player, inputProvider)
        {
            
        }
        
        protected override void BuildItems(List<MenuItemData> target)
        {
            var statusEffectsHeld = Player.UnitHeldStatusEffectsData.All;
            
            foreach (var data in statusEffectsHeld)
            {
                var item = new StatusEffectMenuItemData(data, () => true, () => { });
                target.Add(item);
            }
        }
    }
}