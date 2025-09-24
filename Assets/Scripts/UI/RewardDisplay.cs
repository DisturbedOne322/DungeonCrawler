using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Gameplay.Combat.Items;
using Gameplay.Combat.Skills;
using Gameplay.Units;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TextMeshProUGUI _rewardAmount;
        [SerializeField] private TextMeshProUGUI _tipText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _fadeInTime;
        [SerializeField] private float _stayTime;
        [SerializeField] private float _fadeOutTime;

        private Sequence _sequence;
        private readonly CompositeDisposable _disposables = new();

        private readonly Queue<Action> _rewardQueue = new();
        private bool _isPlaying;

        [Inject]
        private void Construct(PlayerUnit playerUnit, GameplayData gameplayData)
        {
            _disposables.Add(
                playerUnit.UnitInventoryData.Items
                    .ObserveAdd()
                    .Subscribe(addEvent => EnqueueReward(() => ShowItemReward(addEvent.Value, 1)))
            );

            _disposables.Add(
                playerUnit.UnitSkillsData.Skills
                    .ObserveAdd()
                    .Subscribe(addEvent => EnqueueReward(() => ShowSkillReward(addEvent.Value)))
            );

            _disposables.Add(
                gameplayData.Coins.SkipLatestValueOnSubscribe().Subscribe(coins =>
                    EnqueueReward(() => ShowCoinReward(null, coins)))
            );
        }

        private void OnDestroy() => _disposables.Dispose();

        private void EnqueueReward(Action showAction)
        {
            _rewardQueue.Enqueue(showAction);
            
            if (!_isPlaying)
                PlayNextReward();
        }

        private void PlayNextReward()
        {
            if (_rewardQueue.Count == 0)
            {
                _isPlaying = false;
                return;
            }

            _isPlaying = true;
            var action = _rewardQueue.Dequeue();
            action.Invoke();
        }

        private void ShowSkillReward(BaseSkill skill) => ShowReward(skill.Icon, $"YOU LEARNED:\n{skill.Name}");

        private void ShowItemReward(BaseItem item, int amount) => ShowReward(item.Icon, $"YOU GOT:\n{item.Name}", amount);

        private void ShowCoinReward(Sprite sprite, int amount) => ShowReward(sprite, "YOU FOUND COINS!", amount);

        private void ShowReward(Sprite icon, string tip, int amount = 0)
        {
            SetData(icon, tip, amount);
            SetupAnimation();
        }

        private void SetData(Sprite icon, string tip, int amount)
        {
            _rewardImage.sprite = icon;
            _tipText.text = tip;

            _rewardAmount.text = "+" + amount;
            _rewardAmount.gameObject.SetActive(amount > 0);
        }

        private void SetupAnimation()
        {
            _sequence?.Kill();
            _canvasGroup.alpha = 0;

            _sequence = DOTween.Sequence();
            _sequence.Append(_canvasGroup.DOFade(1, _fadeInTime));
            _sequence.AppendInterval(_stayTime);
            _sequence.Append(_canvasGroup.DOFade(0, _fadeOutTime));

            _sequence.OnComplete(PlayNextReward);
            _sequence.SetLink(gameObject);
        }
    }
}