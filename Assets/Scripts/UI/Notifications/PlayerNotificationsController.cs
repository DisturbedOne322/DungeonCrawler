using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Consumables;
using Gameplay.Equipment;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Notifications
{
    public class PlayerNotificationsController : MonoBehaviour
    {
        [SerializeField] private GameItemNotificationDisplay _gameItemNotificationDisplay;
        [SerializeField] private SkillSlotsNotificationDisplay _skillSlotsNotificationDisplay;
        [SerializeField] private ConsumableNotificationDisplay _consumableNotificationDisplay;

        private readonly CompositeDisposable _disposables = new();
        private NotificationsPlayer _notificationsPlayer;

        private PlayerUnit _player;
        private PlayerSkillSlotsManager _skillSlotsManager;

        private void Awake()
        {
            _notificationsPlayer = new NotificationsPlayer(gameObject.GetCancellationTokenOnDestroy());
        }

        //in start to subscribe only after player receives starting inventory
        private void Start()
        {
            Subscribe(_player.UnitSkillsContainer.Skills.ObserveAdd(), EnqueueSkillNotification);
            Subscribe(_player.UnitEquipmentData.OnWeaponEquipped, EnqueueEquipmentNotification);
            Subscribe(_player.UnitEquipmentData.OnArmorEquipped, EnqueueEquipmentNotification);
            Subscribe(_player.UnitHeldStatusEffectsContainer.All.ObserveAdd(), EnqueueStatusEffectNotification);
            Subscribe(_skillSlotsManager.OnSkillSlotsAdded, EnqueueSkillSlotNotification);
            SubscribeConsumables(_player);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        [Inject]
        private void Construct(PlayerUnit player, PlayerSkillSlotsManager playerSkillSlotsManager)
        {
            _player = player;
            _skillSlotsManager = playerSkillSlotsManager;
        }

        private void Subscribe<T>(IObservable<T> stream, Action<T> handler)
        {
            stream.Subscribe(handler).AddTo(_disposables);
        }

        private void SubscribeConsumables(PlayerUnit unit)
        {
            unit.UnitInventoryData.Consumables
                .ObserveAdd()
                .Buffer(TimeSpan.FromMilliseconds(50))
                .Where(buffer => buffer.Count > 0)
                .Subscribe(addEvents =>
                {
                    foreach (var g in addEvents.GroupBy(ev => ev.Value))
                        EnqueueConsumableNotification(g.Key, g.Count());
                })
                .AddTo(_disposables);
        }

        private void EnqueueSkillNotification(CollectionAddEvent<BaseSkill> @event)
        {
            EnqueueNotification(new NotificationData(
                () => _gameItemNotificationDisplay.SetData(@event.Value),
                _gameItemNotificationDisplay.Show)
            );
        }

        private void EnqueueEquipmentNotification(BaseEquipmentPiece equipment)
        {
            EnqueueNotification(new NotificationData(
                () => _gameItemNotificationDisplay.SetData(equipment),
                _gameItemNotificationDisplay.Show)
            );
        }

        private void EnqueueStatusEffectNotification(CollectionAddEvent<HeldStatusEffectData> @event)
        {
            EnqueueNotification(new NotificationData(
                () => _gameItemNotificationDisplay.SetData(@event.Value.Effect),
                _gameItemNotificationDisplay.Show)
            );
        }

        private void EnqueueConsumableNotification(BaseConsumable consumable, int amount)
        {
            EnqueueNotification(new NotificationData(
                () => _consumableNotificationDisplay.SetData(consumable, amount),
                _consumableNotificationDisplay.Show)
            );
        }

        private void EnqueueSkillSlotNotification(int amount)
        {
            EnqueueNotification(new NotificationData(
                () => _skillSlotsNotificationDisplay.SetData(amount),
                _skillSlotsNotificationDisplay.Show)
            );
        }

        private void EnqueueNotification(NotificationData notification)
        {
            _notificationsPlayer.EnqueueNotification(notification);
        }
    }
}