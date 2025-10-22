using System;
using DG.Tweening;
using Gameplay.Player;
using TMPro;
using UI.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.Experience
{
    public class SkillSlotUnlockNotificationDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _notificationText;

        [SerializeField] private FadeOptions _fadeIn;
        [SerializeField] private float _stayTime;
        [SerializeField] private FadeOptions _fadeOut;

        private PlayerSkillSlotsManager _playerSkillSlotsManager;

        private Sequence _sequence;

        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = _playerSkillSlotsManager.OnSkillSlotsAdded.Subscribe(ShowSkillSlotsNotification);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }

        [Inject]
        private void Construct(PlayerSkillSlotsManager playerSkillSlotsManager)
        {
            _playerSkillSlotsManager = playerSkillSlotsManager;
        }

        private void ShowSkillSlotsNotification(int amount)
        {
            _notificationText.text = "NEW SKILL SLOTS: " + amount;

            var width = _rectTransform.sizeDelta.x;
            _sequence = DOTween.Sequence();

            _sequence.Append(_rectTransform.DOAnchorPosX(-width, _fadeIn.FadeDuration).SetEase(_fadeIn.Ease));
            _sequence.AppendInterval(_stayTime);
            _sequence.Append(_rectTransform.DOAnchorPosX(0, _fadeOut.FadeDuration).SetEase(_fadeOut.Ease));
        }
    }
}