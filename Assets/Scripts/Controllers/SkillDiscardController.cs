using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using StateMachine.BattleMenu;
using UI;
using UI.BattleMenu;
using UI.Gameplay;
using UniRx;

namespace Controllers
{
    public class SkillDiscardController
    {
        private readonly UIFactory _uiFactory;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly SkillDiscardMenuUpdater _skillDiscardMenuUpdater;
        private readonly PlayerUnit _playerUnit;

        private readonly List<MenuItemData> _playerSkills = new();
        
        private CompositeDisposable _disposables;
        
        private BaseSkill _skillToDiscard;
        
        private SkillDiscardPopup _skillDiscardPopup;
        
        public SkillDiscardController(UIFactory uiFactory,
            PlayerInputProvider playerInputProvider,
            PlayerUnit playerUnit)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
            _playerUnit = playerUnit;

            _skillDiscardMenuUpdater = new();
        }

        public async UniTask<BaseSkill> MakeSkillDiscardChoice(BaseSkill newSkill)
        {
            Initialize(newSkill);

            await UniTask.WaitUntil(() => _skillToDiscard != null);
            
            Dispose();

            await _skillDiscardPopup.HidePopup();
            
            return _skillToDiscard;
        }

        private void Initialize(BaseSkill newSkill)
        {
            _skillToDiscard = null;

            CreateItemsSelection(newSkill);
            SubscribeToInputEvents();
            ShowPopup();
            
            _playerInputProvider.EnableUiInput(true);
        }

        private void Dispose()
        {
            _playerInputProvider.EnableUiInput(false);
            _disposables.Dispose();
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
            {
                _playerSkills.Add(
                    MenuItemData.ForSkill(
                        skill,
                        () => true,
                        () => _skillToDiscard = skill)
                );
            }

            var newSkillData = MenuItemData.ForSkill(
                newSkill,
                () => true,
                () => _skillToDiscard = newSkill);
            
            _skillDiscardMenuUpdater.SetMenuItems(_playerSkills);
            _skillDiscardMenuUpdater.SetNewSkill(newSkillData);
            _skillDiscardMenuUpdater.ResetSelection();
        }

        private void SubscribeToInputEvents()
        {
            _disposables = new();
            
            _disposables.Add(_playerInputProvider.OnUiSubmit.Subscribe(_ => _skillDiscardMenuUpdater.ExecuteSelection()));            
            _disposables.Add(_playerInputProvider.OnUiUp.Subscribe(_ => _skillDiscardMenuUpdater.ProcessInput(-1, 0)));
            _disposables.Add(_playerInputProvider.OnUiDown.Subscribe(_ => _skillDiscardMenuUpdater.ProcessInput(+1, 0)));
            _disposables.Add(_playerInputProvider.OnUiLeft.Subscribe(_ => _skillDiscardMenuUpdater.ProcessInput(0, -1)));
            _disposables.Add(_playerInputProvider.OnUiRight.Subscribe(_ => _skillDiscardMenuUpdater.ProcessInput(0, +1)));
        }
    }
}