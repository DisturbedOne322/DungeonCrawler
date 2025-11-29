using Animations;
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
        [SerializeField] private MenuPageSelectAnimator _animator;
        
        private VerticalUiNavigator _uiNavigator;
        
        [Inject]
        private void Construct(VerticalUiNavigator uiNavigator)
        {
            _uiNavigator = uiNavigator;
        }

        private void Start()
        {
            for (var i = 0; i < _items.Length; i++)
            {
                var item = _items[i];
                int index = i;
                
                var page = _pages[i];
                
                _uiNavigator.AddMenuItem(item, () =>
                {
                    _animator.Select(page.Image);
                    _pages[index].Select();
                });

                page.OnBack += () =>
                {
                    _animator.Deselect(page.Image);
                    page.Deselect();
                };
            }
        }
    }
}