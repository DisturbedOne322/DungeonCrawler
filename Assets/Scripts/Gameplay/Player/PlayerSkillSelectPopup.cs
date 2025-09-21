using System;
using System.Collections.Generic;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using Gameplay.Units;
using TMPro.EditorUtilities;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerSkillSelectPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private SkillSelectView _prefab;
        
        private UnitSkillsData _unitSkillsData;
        private PlayerInputProvider _playerInputProvider;
        private CombatService _combatService;

        private int _selectedSkillIndex = 0;
        
        private List<SkillSelectView> _skillSelectViews;

        private CompositeDisposable _disposables;
        
        public event Action<BaseSkill> OnSkillSelected;

        [Inject]
        private void Construct(PlayerUnit player, 
            PlayerInputProvider playerInputProvider, 
            CombatService combatService)
        {
            _unitSkillsData = player.UnitSkillsData;
            _playerInputProvider = playerInputProvider;
            _combatService = combatService;
        }
        
        private void Awake()
        {
            _playerInputProvider.EnableUiInput(true);

            _disposables = new();
            _disposables.Add(_playerInputProvider.OnUiDown.Subscribe(_ => ScrollSelection(+1)));
            _disposables.Add(_playerInputProvider.OnUiUp.Subscribe(_ => ScrollSelection(-1)));
            _disposables.Add(_playerInputProvider.OnUiSubmit.Subscribe(_ => TrySubmitSelection()));
            
            CreateSelection();
        }

        private void OnDestroy()
        {
            _playerInputProvider.EnableUiInput(false);
            _disposables.Dispose();
        }

        private void ScrollSelection(int direction)
        {
            int skillsAmount = _unitSkillsData.Skills.Count;

            do
            {
                _selectedSkillIndex += direction;
                if(_selectedSkillIndex < 0)
                    _selectedSkillIndex = skillsAmount - 1;
            
                if(_selectedSkillIndex >= skillsAmount)
                    _selectedSkillIndex = 0;
                
            } while (!IsSkillUsable(_selectedSkillIndex));
            
            UpdateSelection();
        }

        private void TrySubmitSelection()
        {
            OnSkillSelected?.Invoke(_unitSkillsData.Skills[_selectedSkillIndex]);
            Destroy(gameObject);
        }

        private void CreateSelection()
        {
            _skillSelectViews = new();
            var skills = _unitSkillsData.Skills;
            
            for (var i = 0; i < skills.Count; i++)
            {
                var skill = skills[i];
                var view = Instantiate(_prefab, _content);

                view.SetData(skill);
                
                if (i == 0)
                    view.SetSelected(true);
                
                if(!skill.CanUse(_combatService))
                    view.SetUnusable();
                
                _skillSelectViews.Add(view);
            }
        }

        private void UpdateSelection()
        {
            for (int i = 0; i < _skillSelectViews.Count; i++)
            {
                var view = _skillSelectViews[i];
                
                if (!IsSkillUsable(i))
                {
                    view.SetUnusable();
                    continue;
                }
                
                view.SetSelected(i == _selectedSkillIndex);
            }
        }

        private bool IsSkillUsable(int index) => 
            _unitSkillsData.Skills[index].CanUse(_combatService);
    }
}