using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Combat;
using StateMachine.BattleMenu;
using UI.BattleMenu;
using UI.Core;
using UnityEngine;

namespace UI
{
    public class SkillDiscardPopup : BasePopup
    {
        [SerializeField] private CanvasGroup _canvas;
        
        [SerializeField] private RectTransform _oldSkillsParent;
        [SerializeField] private RectTransform _newSkillParent;
        
        [SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        [SerializeField] private SkillMenuItemView _skillPrefab;
        
        protected override void InitializePopup()
        {
            
        }

        public override void ShowPopup()
        {
            _canvas.DOFade(1, _showTime).SetEase(Ease.OutBack).SetLink(gameObject);
        }

        public override void HidePopup()
        {
            _canvas.DOKill();
            _canvas.DOFade(0, _hideTime).
                SetEase(Ease.OutBack).
                SetLink(gameObject).
                OnComplete(DestroyPopup);
        }
        
        public void SetData(SkillDiscardMenuUpdater skillDiscardMenuUpdater)
        {
            var playerSkills = skillDiscardMenuUpdater.MenuItems;
            var newSkill = skillDiscardMenuUpdater.NewSkillData;
            
            foreach (var skill in playerSkills)
            {
                var view = Instantiate(_skillPrefab, _oldSkillsParent, false);
                view.Bind(skill);
                view.SetDescription(skill.Description);
            }
            
            var newView = Instantiate(_skillPrefab, _newSkillParent, false);
            newView.Bind(newSkill);
            newView.SetDescription(newSkill.Description);
        }
    }
}