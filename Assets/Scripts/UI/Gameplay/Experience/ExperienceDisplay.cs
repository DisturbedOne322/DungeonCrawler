using System;
using DG.Tweening;
using Gameplay.Experience;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Gameplay.Experience
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _experienceText;
        
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private float _progressAnimDuration = 0.33f;
        
        private Sequence _experienceSequence;
        private Sequence _progressSequence;

        private readonly CompositeDisposable _disposables = new();
        
        [Inject]
        private void Construct(ExperienceData experienceData)
        {
            experienceData.OnExperienceGained.Subscribe(ShowExperienceGained).AddTo(_disposables);
            experienceData.OnLevelUp.Subscribe(ShowLevelUp).AddTo(_disposables);
            experienceData.OnProgressChanged.Subscribe(ShowProgress).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void ShowExperienceGained(int amount)
        { 
            _experienceSequence?.Kill();
            _experienceSequence = DOTween.Sequence();

            _experienceText.text = $"+{amount} EXP";
            _experienceText.transform.localScale = Vector3.zero;

            _experienceSequence.Append(_experienceText.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.InOutBounce));
            _experienceSequence.AppendInterval(1f);
            _experienceSequence.Append(_experienceText.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutCubic));
            _experienceSequence.SetLink(gameObject);
        }

        private void ShowLevelUp(int currentLevel)
        {
            _currentLevelText.text = "LVL: " + currentLevel;
        }

        private void ShowProgress(float newProgress)
        {
            _progressSequence?.Kill();
            
            float currentProgress = _progressSlider.value;

            _progressSequence = DOTween.Sequence();
            _progressSequence.SetLink(gameObject);
            _progressSequence.SetEase(Ease.InOutCirc);
            
            if (newProgress > currentProgress)
            {
                _progressSequence.Append(_progressSlider.DOValue(newProgress, _progressAnimDuration));
                return;
            }

            float t1 = _progressAnimDuration * (1 - currentProgress);
            float t2 = _progressAnimDuration * newProgress;
            
            _progressSequence.Append(_progressSlider.DOValue(1, t1));
            _progressSequence.Append(_progressSlider.DOValue(newProgress, t2));
        }
    }
}