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
                if(data.Description != null)
                    views.Add(CreateSkillMenuItem(data));
                else
                    views.Add(CreateMenuItem(data));
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
    }
}