using UI.Core;
using UI.InventoryDisplay;
using UI.Navigation;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class PausePopup : BasePopup
    {
        [SerializeField] private MenuItemDataMono[] _items;
        [SerializeField] private BaseDisplayMenuView[] _pages;
        
        private VerticalUiNavigator _uiNavigator;
        
        [Inject]
        private void Construct(VerticalUiNavigator uiNavigator)
        {
            _uiNavigator = uiNavigator;
            _uiNavigator.TakeControl();
        }

        private void Start()
        {
            for (var i = 0; i < _items.Length; i++)
            {
                var item = _items[i];
                int index = i;
                
                var page = _pages[i];
                
                _uiNavigator.AddMenuItem(item, () => _pages[index].Select());
                page.OnBack += () => page.Deselect();
            }
        }
    }
}