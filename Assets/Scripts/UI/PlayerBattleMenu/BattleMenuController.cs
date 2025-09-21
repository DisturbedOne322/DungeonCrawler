using System;
using System.Collections.Generic;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using Gameplay.Player;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace UI.PlayerBattleMenu
{
    public class BattleMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainMenuItemsParent;
        [SerializeField] private RectTransform _skillItemsParent;
        
        private MainBattleBaseMenuState _mainState;
        private SkillsMenuState _skillsState;

        private PlayerInputProvider _playerInputProvider;
        private UnitSkillsData _skillsData;
        private MenuItemViewFactory _factory;
        private CombatService _combatService;
        
        public event Action<BaseSkill> OnSkillSelected;
        
        [Inject]
        private void Construct(PlayerUnit player, 
            PlayerInputProvider playerInputProvider,
            CombatService combatService,
            MenuItemViewFactory menuItemViewFactory,
            MainBattleBaseMenuState mainState,
            SkillsMenuState skillsState)
        {
            _skillsData = player.UnitSkillsData;
            _playerInputProvider = playerInputProvider;
            _combatService = combatService;
            _factory = menuItemViewFactory;
            _skillsState = skillsState;
            _mainState = mainState;
        }

        private void Start()
        {
            CreateMenuItems();
            CreateSkillItems();
            
            _mainState.EnterState();
            
            _playerInputProvider.EnableUiInput(true);
        }

        private void CreateMenuItems()
        {
            List<MenuItemView> mainMenuItems = new List<MenuItemView>();
            
            var attackSkillItem = _factory.CreateSkillItemView(_skillsData.BasicAttackSkill);
            var attackSkillData = new MenuItemData()
            {
                OnSelected = () => SelectSkill(_skillsData.BasicAttackSkill),
                Selectable = () => _skillsData.BasicAttackSkill.CanUse(_combatService),
            };
            attackSkillItem.SetData(attackSkillData);
            attackSkillItem.transform.SetParent(_mainMenuItemsParent, false);
            mainMenuItems.Add(attackSkillItem);


            var guardSkillItem = _factory.CreateSkillItemView(_skillsData.GuardSkill);
            var guardSkillData = new MenuItemData()
            {
                OnSelected = () => SelectSkill(_skillsData.GuardSkill),
                Selectable = () => _skillsData.GuardSkill.CanUse(_combatService),
            };
            guardSkillItem.SetData(guardSkillData);
            guardSkillItem.transform.SetParent(_mainMenuItemsParent, false);
            mainMenuItems.Add(guardSkillItem);

            var skillsMenuItem = _factory.CreateMenuItemView("Skills");
            var skillsData = new MenuItemData()
            {
                OnSelected = OpenSkillsMenu,
                Selectable = () => true,
            };
            skillsMenuItem.SetData(skillsData);
            skillsMenuItem.transform.SetParent(_mainMenuItemsParent, false);
            mainMenuItems.Add(skillsMenuItem);

            _mainState.Initialize(mainMenuItems);
        }

        private void CreateSkillItems()
        {
            var skills = _skillsData.Skills;

            List<MenuItemView> skillItems = new();
            
            foreach (var skill in skills)
            {
                var view = _factory.CreateSkillItemView(skill);
                var data = new MenuItemData()
                {
                    OnSelected = () => SelectSkill(skill),
                    Selectable = () => skill.CanUse(_combatService)
                };
                
                view.SetData(data);
                skillItems.Add(view);
                view.transform.SetParent(_skillItemsParent, false);
            }
            
            _skillsState.Initialize(skillItems);
        }

        private void OpenSkillsMenu()
        {
            _mainMenuItemsParent.gameObject.SetActive(false);
            _skillItemsParent.gameObject.SetActive(true);
            
            _mainState.ExitState();
            _skillsState.EnterState();
        }

        public void ReturnToMainMenu()
        {
            _skillItemsParent.gameObject.SetActive(false);
            _mainMenuItemsParent.gameObject.SetActive(true);

            _skillsState.ExitState();
            _mainState.EnterState();
        }

        private void SelectSkill(BaseSkill skill)
        {
            OnSkillSelected?.Invoke(skill);
            _playerInputProvider.EnableUiInput(false);
            
            _mainState.ExitState();
            _skillsState.ExitState();
            
            Destroy(gameObject);
        }
    }
}