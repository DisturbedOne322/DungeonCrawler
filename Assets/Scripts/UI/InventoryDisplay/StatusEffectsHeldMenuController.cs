using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus;
using UI.Menus.Data;

namespace UI.InventoryDisplay
{
    public class StatusEffectsHeldMenuController : BaseUIInputHandler
    {
        private readonly PlayerUnit _player;
        private readonly PlayerInputProvider _playerInputProvider;

        private readonly MenuItemsUpdater _itemsUpdater = new();
        private readonly List<MenuItemData> _items = new();
        
        public MenuItemsUpdater ItemsUpdater => _itemsUpdater;
        public List<MenuItemData> Items => _items;

        public StatusEffectsHeldMenuController(PlayerUnit player, 
            PlayerInputProvider playerInputProvider)
        {
            _player = player;
            _playerInputProvider = playerInputProvider;
        }

        public void TakeControls()
        {
            _playerInputProvider.AddUiInputOwner(this);
        }

        public void RemoveControls()
        {
            _playerInputProvider.RemoveUiInputOwner(this);
        }
        
        public void CreateMenu()
        {
            _items.Clear();
            var statusEffectsHeld = _player.UnitHeldStatusEffectsData.All;
            
            foreach (var data in statusEffectsHeld)
            {
                var item = new StatusEffectMenuItemData(data, () => true, () => { });
                _items.Add(item);
            }
            
            _itemsUpdater.SetMenuItems(_items);
        }
        
        public override void OnUIUp() => _itemsUpdater.UpdateSelection(-1);

        public override void OnUIDown() => _itemsUpdater.UpdateSelection(+1);
    }
}