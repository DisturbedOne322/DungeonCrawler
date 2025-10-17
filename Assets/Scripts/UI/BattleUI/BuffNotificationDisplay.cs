using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.Units;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace UI.BattleUI
{
    public class BuffNotificationDisplay : MonoBehaviour
    {
        [SerializeField] private LocalizedString _chargedBuff;
        [SerializeField] private LocalizedString _concentratedBuff;
        [SerializeField] private LocalizedString _guardedBuff;

        [SerializeField] private TextDisplay _textDisplay;

        private CombatEventsBus _combatEventsBus;
        private CompositeDisposable _combatSubscriptions;
        private PlayerUnit _playerUnit;

        private CompositeDisposable _unitSubscriptions;

        private void Awake()
        {
            _combatSubscriptions = new CompositeDisposable();
            _combatSubscriptions.Add(_combatEventsBus.OnCombatStarted.Subscribe(enemy =>
            {
                _unitSubscriptions = new CompositeDisposable();
                SubscribeToUnit(_playerUnit);
                SubscribeToUnit(enemy);
            }));

            _combatSubscriptions.Add(_combatEventsBus.OnCombatEnded.Subscribe(_ => { _unitSubscriptions.Dispose(); }));
        }

        private void OnDestroy()
        {
            _combatSubscriptions.Dispose();
        }

        [Inject]
        private void Construct(CombatEventsBus combatEventsBus, PlayerUnit playerUnit)
        {
            _combatEventsBus = combatEventsBus;
            _playerUnit = playerUnit;
        }

        private void SubscribeToUnit(IGameUnit gameUnit)
        {
            var unitName = gameUnit.EntityName;
            var buffsData = gameUnit.UnitBuffsData;

            /*_unitSubscriptions.Add(
                buffsData.Guarded.Subscribe(buffed =>
                {
                    if (buffed)
                        DisplayBuff(unitName, _guardedBuff.GetLocalizedString());
                }));

            _unitSubscriptions.Add(
                buffsData.Concentrated.Subscribe(buffed =>
                {
                if (buffed)
                    DisplayBuff(unitName, _concentratedBuff.GetLocalizedString());
                }));

            _unitSubscriptions.Add(
                buffsData.Charged.Subscribe(buffed =>
                {
                if (buffed)
                    DisplayBuff(unitName, _chargedBuff.GetLocalizedString());
                }));*/
        }

        private void DisplayBuff(string unitName, string buffText)
        {
            _textDisplay.ShowText($"{unitName} {buffText}!");
        }
    }
}