using System;
using UI.BattleMenu;
using UnityEngine;

namespace UI.Navigation
{
    [RequireComponent(typeof(BaseMenuItemView))]
    public class MenuItemDataMono : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private BaseMenuItemView _baseMenuItemView;
        
        public MenuItemData CreateData(Action callback)
        {
            var data = MenuItemData.ForNavigationItem(
                _name,
                callback
            );
            
            _baseMenuItemView.Bind(data);
            return data;
        }
    }
}