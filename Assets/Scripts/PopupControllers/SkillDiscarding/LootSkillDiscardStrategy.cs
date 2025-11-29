using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using UI;
using UI.BattleMenu;
using UI.Gameplay;
using UI.Gameplay.Experience;
using UI.Menus.Data;

namespace PopupControllers.SkillDiscarding
{
    public class LootSkillDiscardStrategy : BaseUIInputHandler, ISkillDiscardStrategy
    {
        private readonly PlayerInputProvider _playerInputProvider;

        private readonly PlayerUnit _playerUnit;
        private readonly SkillDiscardMenuUpdater _skillDiscardMenuUpdater;
        private readonly UIFactory _uiFactory;

        private SkillLootDiscardPopup _skillLootDiscardPopup;

        private BaseSkill _skillToDiscard;

        public LootSkillDiscardStrategy(UIFactory uiFactory,
            PlayerInputProvider playerInputProvider,
            PlayerUnit playerUnit)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
            _playerUnit = playerUnit;

            _skillDiscardMenuUpdater = new SkillDiscardMenuUpdater();
        }

        public async UniTask<BaseSkill> MakeSkillDiscardChoice(BaseSkill newSkill)
        {
            Initialize(newSkill);

            await _playerInputProvider.EnableUIInputUntil(UniTask.WaitUntil(() => _skillToDiscard != null), this);

            await _skillLootDiscardPopup.HidePopup();

            return _skillToDiscard;
        }

        private void Initialize(BaseSkill newSkill)
        {
            _skillToDiscard = null;

            CreateItemsSelection(newSkill);
            ShowPopup();
        }

        private void ShowPopup()
        {
            _skillLootDiscardPopup = _uiFactory.CreatePopup<SkillLootDiscardPopup>();
            _skillLootDiscardPopup.SetData(_skillDiscardMenuUpdater);
            _skillLootDiscardPopup.ShowPopup().Forget();
        }

        private void CreateItemsSelection(BaseSkill newSkill)
        {
            List<MenuItemData> playerSkills = new();

            var skillsData = _playerUnit.UnitSkillsData;

            foreach (var skill in skillsData.Skills)
                playerSkills.Add(
                    MenuItemData.ForSkillItem(
                        skill,
                        () => true,
                        () => _skillToDiscard = skill)
                );

            var newSkillData = MenuItemData.ForSkillItem(
                newSkill,
                () => true,
                () => _skillToDiscard = newSkill);

            _skillDiscardMenuUpdater.SetNewSkill(newSkillData);
            _skillDiscardMenuUpdater.SetMenuItems(playerSkills);
        }

        public override void OnUISubmit()
        {
            _skillDiscardMenuUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            _skillDiscardMenuUpdater.ProcessInput(-1, 0);
        }

        public override void OnUIDown()
        {
            _skillDiscardMenuUpdater.ProcessInput(+1, 0);
        }

        public override void OnUILeft()
        {
            _skillDiscardMenuUpdater.ProcessInput(0, -1);
        }

        public override void OnUIRight()
        {
            _skillDiscardMenuUpdater.ProcessInput(0, +1);
        }
    }
}