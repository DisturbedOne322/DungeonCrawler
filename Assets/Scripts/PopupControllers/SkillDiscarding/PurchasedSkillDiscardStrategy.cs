using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using UI;
using UI.Menus;
using UI.Menus.Data;
using UI.Popups;

namespace PopupControllers.SkillDiscarding
{
    public class PurchasedSkillDiscardStrategy : BaseUIInputHandler, ISkillDiscardStrategy
    {
        private readonly MenuItemsUpdater _menuItemsUpdater;
        private readonly PlayerInputProvider _playerInputProvider;

        private readonly PlayerUnit _playerUnit;
        private readonly UIFactory _uiFactory;

        private SkillPurchaseDiscardPopup _popup;

        private BaseSkill _skillToDiscard;

        public PurchasedSkillDiscardStrategy(UIFactory uiFactory,
            PlayerInputProvider playerInputProvider,
            PlayerUnit playerUnit)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
            _playerUnit = playerUnit;

            _menuItemsUpdater = new MenuItemsUpdater();
        }

        public async UniTask<BaseSkill> MakeSkillDiscardChoice(BaseSkill newSkill)
        {
            Initialize();

            await _playerInputProvider.EnableUIInputUntil(UniTask.WaitUntil(() => _skillToDiscard != null), this);

            await _popup.HidePopup();

            return _skillToDiscard;
        }

        private void Initialize()
        {
            _skillToDiscard = null;

            CreateItemsSelection();
            ShowPopup();
        }

        private void ShowPopup()
        {
            _popup = _uiFactory.CreatePopup<SkillPurchaseDiscardPopup>();
            _popup.SetData(_menuItemsUpdater);
            _popup.ShowPopup().Forget();
        }

        private void CreateItemsSelection()
        {
            List<MenuItemData> playerSkills = new();

            var skillsData = _playerUnit.UnitSkillsContainer;

            foreach (var skill in skillsData.Skills)
                playerSkills.Add(
                    MenuItemData.ForSkillItem(
                        skill,
                        () => true,
                        () => _skillToDiscard = skill)
                );

            _menuItemsUpdater.SetMenuItems(playerSkills);
        }

        public override void OnUISubmit()
        {
            _menuItemsUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            _menuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            _menuItemsUpdater.UpdateSelection(+1);
        }
    }
}