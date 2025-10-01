using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using Gameplay.Combat;
using Gameplay.Combat.Consumables;
using Gameplay.Combat.Skills;
using Gameplay.Units;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Gameplay
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
            SubscribeToConsumablesAddEvents(playerUnit);
            SubscribeToSkillsAddEvents(playerUnit);
            SubscribeToCoinsAddEvents(gameplayData);
            SubscribeToWeaponAddEvent(playerUnit);
            SubscribeToArmorAddEvent(playerUnit);
        }

        private void OnDestroy() => _disposables.Dispose();

        private void SubscribeToConsumablesAddEvents(PlayerUnit playerUnit)
        {
            _disposables.Add(
                playerUnit.UnitInventoryData.Consumables
                    .ObserveAdd()
                    .Buffer(TimeSpan.FromMilliseconds(50))
                    .Where(buffer => buffer.Count > 0)
                    .Subscribe(addEvents =>
                    {
                        var grouped = addEvents
                            .GroupBy(ev => ev.Value)
                            .Select(g => new { Item = g.Key, Count = g.Count() });

                        foreach (var entry in grouped)
                            EnqueueReward(() => ShowItemReward(entry.Item, entry.Count));
                    }));
        }

        private void SubscribeToSkillsAddEvents(PlayerUnit playerUnit)
        {
            _disposables.Add(
                playerUnit.UnitSkillsData.Skills
                    .ObserveAdd()
                    .Subscribe(addEvent => EnqueueReward(() => ShowSkillReward(addEvent.Value)))
            );
        }

        private void SubscribeToCoinsAddEvents(GameplayData gameplayData)
        {
            _disposables.Add(
                gameplayData.Coins
                    .SkipLatestValueOnSubscribe()
                    .Pairwise()
                    .Subscribe(pair =>
                    {
                        int prev = pair.Previous;
                        int current = pair.Current;
                        int added = current - prev;

                        if (added > 0)
                            EnqueueReward(() => ShowCoinReward(null, added));
                    })
            );

        }

        private void SubscribeToWeaponAddEvent(PlayerUnit playerUnit)
        {
            _disposables.Add(
                playerUnit.UnitEquipmentData.OnWeaponEquipped.Subscribe(weapon => 
                    EnqueueReward(() => ShowEquipmentReward(weapon)))
                );
        }
        
        private void SubscribeToArmorAddEvent(PlayerUnit playerUnit)
        {
            _disposables.Add(
                playerUnit.UnitEquipmentData.OnArmorEquipped.Subscribe(armor => 
                    EnqueueReward(() => ShowEquipmentReward(armor)))
            );
        }

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

        private void ShowItemReward(BaseConsumable consumable, int amount) => ShowReward(consumable.Icon, $"YOU GOT:\n{consumable.Name}", amount);

        private void ShowCoinReward(Sprite sprite, int amount) => ShowReward(sprite, "YOU FOUND COINS!", amount);
        private void ShowEquipmentReward(BaseGameItem item) => ShowReward(item.Icon, $"YOU GOT: \n {item.Name}");

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