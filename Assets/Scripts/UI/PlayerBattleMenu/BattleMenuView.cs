using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using Gameplay.Player;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace UI.PlayerBattleMenu
{
    public class BattleMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainMenuParent;
        [SerializeField] private RectTransform _skillsMenuParent;

        private BaseMenuState _currentState;
        private SelectableMenuState _mainState;
        private SelectableMenuState _skillsState;

        private PlayerInputProvider _input;
        private UnitSkillsData _skillsData;
        private MenuItemViewFactory _factory;
        private CombatService _combat;

        public event Action<BaseSkill> OnSkillSelected;

        [Inject]
        private void Construct(PlayerUnit player,
            PlayerInputProvider input,
            CombatService combat,
            MenuItemViewFactory factory)
        {
            _skillsData = player.UnitSkillsData;
            _input = input;
            _combat = combat;
            _factory = factory;
        }

        private void Start()
        {
            var mainItems = BuildMainMenuItems();
            var skillItems = BuildSkillsMenuItems();

            _mainState = new SelectableMenuState(_input, this);
            _mainState.Initialize(mainItems);

            _skillsState = new SelectableMenuState(_input, this, true);
            _skillsState.Initialize(skillItems);

            OpenMainMenu();
            _input.EnableUiInput(true);
        }

        private List<MenuItemView> BuildMainMenuItems()
        {
            var items = new List<MenuItemView>
            {
                CreateSkillItem(_skillsData.BasicAttackSkill),
                CreateSkillItem(_skillsData.GuardSkill),
                CreateMenuItem("Skills", OpenSkillsMenu)
            };
            return items;
        }

        private List<MenuItemView> BuildSkillsMenuItems()
        {
            return _skillsData.Skills
                .Select(CreateSkillItem)
                .ToList();
        }

        private MenuItemView CreateSkillItem(BaseSkill skill)
        {
            var view = _factory.CreateSkillItemView(skill);
            view.SetData(MenuItemData.ForSkill(skill, _combat, SelectSkill));
            view.transform.SetParent(skill == _skillsData.BasicAttackSkill || skill == _skillsData.GuardSkill
                ? _mainMenuParent
                : _skillsMenuParent, false);
            return view;
        }

        private MenuItemView CreateMenuItem(string label, Action action)
        {
            var view = _factory.CreateMenuItemView(label);
            view.SetData(MenuItemData.Simple(label, action));
            view.transform.SetParent(_mainMenuParent, false);
            return view;
        }

        private void OpenSkillsMenu()
        {
            _mainMenuParent.gameObject.SetActive(false);
            _skillsMenuParent.gameObject.SetActive(true);

            ChangeState(_skillsState);
        }

        public void OpenMainMenu()
        {
            _skillsMenuParent.gameObject.SetActive(false);
            _mainMenuParent.gameObject.SetActive(true);

            ChangeState(_mainState);
        }
        
        private void ChangeState(BaseMenuState state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }

        private void SelectSkill(BaseSkill skill)
        {
            OnSkillSelected?.Invoke(skill);
            _input.EnableUiInput(false);
            _currentState.ExitState();
            
            Destroy(gameObject);
        }
    }
}