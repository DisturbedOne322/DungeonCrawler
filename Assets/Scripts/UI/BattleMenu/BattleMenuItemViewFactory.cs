using System.Collections.Generic;
using Gameplay.Services;
using StateMachine.BattleMenu;
using UnityEngine;
using Zenject;

namespace UI.BattleMenu
{
    public class BattleMenuItemViewFactory : MonoBehaviour
    {
        [SerializeField] private BattleMenuPageView _pagePrefab;
        [SerializeField] private BaseBattleMenuItemView _menuItemPrefab;
        [SerializeField] private SkillBattleMenuItemView _skillMenuItemPrefab;
        [SerializeField] private ItemBattleMenuItemView _itemMenuItemPrefab;

        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }

        public BattleMenuPageView CreatePage() => _factory.Create<BattleMenuPageView>(_pagePrefab.gameObject);

        public List<BaseBattleMenuItemView> CreateViewsForData(List<BattleMenuItemData> dataList)
        {
            List<BaseBattleMenuItemView> views = new List<BaseBattleMenuItemView>();

            foreach (var data in dataList)
            {
                switch (data.Type)
                {
                    case MenuItemType.Submenu:
                        views.Add(CreateMenuItem(data));
                        break;
                    case MenuItemType.Skill:
                        views.Add(CreateSkillMenuItem(data));
                        break;
                    case MenuItemType.Item:
                        views.Add(CreateItemMenuItem(data));
                        break;
                }
            }
            
            return views;
        }
        
        private BaseBattleMenuItemView CreateMenuItem(BattleMenuItemData data)
        {
            var view = _factory.Create<BaseBattleMenuItemView>(_menuItemPrefab.gameObject);
            view.Bind(data);
            
            return view;
        }
        
        private SkillBattleMenuItemView CreateSkillMenuItem(BattleMenuItemData data)
        {
            var view = _factory.Create<SkillBattleMenuItemView>(_skillMenuItemPrefab.gameObject);
            view.Bind(data);
            view.SetDescription(data.Description);
            
            return view;
        }
        
        private ItemBattleMenuItemView CreateItemMenuItem(BattleMenuItemData data)
        {
            var view = _factory.Create<ItemBattleMenuItemView>(_itemMenuItemPrefab.gameObject);
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetQuantity(data.Quantity);
            
            return view;
        }
    }
}