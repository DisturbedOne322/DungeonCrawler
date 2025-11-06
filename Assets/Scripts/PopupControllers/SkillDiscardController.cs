using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using UI;
using UI.BattleMenu;
using UI.Gameplay.Experience;

namespace PopupControllers
{
    public class SkillDiscardController : BaseUIInputHandler
    {
        private readonly PlayerInputProvider _playerInputProvider;

        private readonly List<MenuItemData> _playerSkills = new();
        private readonly PlayerUnit _playerUnit;
        private readonly SkillDiscardMenuUpdater _skillDiscardMenuUpdater;
        private readonly UIFactory _uiFactory;
        
        private SkillDiscardPopup _skillDiscardPopup;

        private BaseSkill _skillToDiscard;

        public SkillDiscardController(UIFactory uiFactory,
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
            
            await _skillDiscardPopup.HidePopup();

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
            _skillDiscardPopup = _uiFactory.CreatePopup<SkillDiscardPopup>();
            _skillDiscardPopup.SetData(_skillDiscardMenuUpdater);
            _skillDiscardPopup.ShowPopup().Forget();
        }

        private void CreateItemsSelection(BaseSkill newSkill)
        {
            _playerSkills.Clear();

            var skillsData = _playerUnit.UnitSkillsData;

            foreach (var skill in skillsData.Skills)
                _playerSkills.Add(
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
            _skillDiscardMenuUpdater.SetMenuItems(_playerSkills);
        }
        
        public override void OnUISubmit() => _skillDiscardMenuUpdater.ExecuteSelection();
        public override void OnUIUp() => _skillDiscardMenuUpdater.ProcessInput(-1, 0);
        public override void OnUIDown() => _skillDiscardMenuUpdater.ProcessInput(+1, 0);
        public override void OnUILeft() => _skillDiscardMenuUpdater.ProcessInput(0, -1);
        public override void OnUIRight() => _skillDiscardMenuUpdater.ProcessInput(0, +1);
    }
}