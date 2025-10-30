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

        private readonly CompositeDisposable _disposables = new ();
        private NotificationsPlayer _notificationsPlayer;
        
        [Inject]
        private void Construct(PlayerUnit unit, PlayerSkillSlotsManager playerSkillSlotsManager)
        {
            Subscribe(unit.UnitSkillsData.Skills.ObserveAdd(), e => EnqueueSkillNotification(e.Value));
            Subscribe(unit.UnitEquipmentData.OnWeaponEquipped, EnqueueEquipmentNotification);
            Subscribe(unit.UnitEquipmentData.OnArmorEquipped, EnqueueEquipmentNotification);
            Subscribe(unit.UnitHeldStatusEffectsData.All.ObserveAdd(), e => EnqueueStatusEffectNotification(e.Value));
            Subscribe(playerSkillSlotsManager.OnSkillSlotsAdded, EnqueueSkillSlotNotification);
            SubscribeConsumables(unit);
        }

        private void Awake() => _notificationsPlayer = new(gameObject.GetCancellationTokenOnDestroy());

        private void OnDestroy() => _disposables.Dispose();

        private void Subscribe<T>(IObservable<T> stream, Action<T> handler) => stream.Subscribe(handler).AddTo(_disposables);

        private void SubscribeConsumables(PlayerUnit unit)
        {
            unit.UnitInventoryData.Consumables
                .ObserveAdd()
                .Buffer(TimeSpan.FromMilliseconds(50))
                .Where(buffer => buffer.Count > 0)
                .Subscribe(addEvents => {
                    foreach (var g in addEvents.GroupBy(ev => ev.Value))
                        EnqueueConsumableNotification(g.Key, g.Count());
                })
                .AddTo(_disposables);
        }
        
        private void EnqueueSkillNotification(BaseSkill skill)
        {
            EnqueueNotification(new NotificationData(
                () => _gameItemNotificationDisplay.SetData(skill),
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

        private void EnqueueStatusEffectNotification(BaseStatusEffectData statusEffectData)
        {
            EnqueueNotification(new NotificationData(
                () => _gameItemNotificationDisplay.SetData(statusEffectData),
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

        private void EnqueueNotification(NotificationData notification) => _notificationsPlayer.EnqueueNotification(notification);
    }
}