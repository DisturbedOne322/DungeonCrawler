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
            var statusEffectsHeld = Player.UnitHeldStatusEffectsContainer.All;

            foreach (var data in statusEffectsHeld)
            {
                var item = new StatusEffectMenuItemData(data.Effect, () => true, () => { });
                target.Add(item);
            }
        }
    }
}