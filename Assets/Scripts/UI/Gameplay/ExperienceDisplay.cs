using DG.Tweening;
using Gameplay.Experience;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _levelUpText;
        [SerializeField] private TextMeshProUGUI _experienceText;

        private Sequence _experienceSequence;
        private Sequence _levelUpSequence;
        
        [Inject]
        private void Construct(ExperienceData experienceData)
        {
            experienceData.OnExperienceGained.Subscribe(DisplayExperience).AddTo(gameObject);
            experienceData.OnLevelUp.Subscribe(ShowLevelUp).AddTo(gameObject);
        }

        private void DisplayExperience(int amount)
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
            PlayLevelUpAnim();
        }

        private void PlayLevelUpAnim()
        {
            _levelUpSequence?.Kill();
            _levelUpSequence = DOTween.Sequence();
            
            _levelUpText.text = "LVL ++";
            _levelUpText.transform.localScale = Vector3.zero;
            
            _levelUpSequence.Append(_levelUpText.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.InOutBounce));
            _levelUpSequence.AppendInterval(1f);
            _levelUpSequence.Append(_levelUpText.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutCubic));
            _levelUpSequence.SetLink(gameObject);
        }
    }
}