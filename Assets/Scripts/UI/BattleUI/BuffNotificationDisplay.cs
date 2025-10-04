using Constants;
using Gameplay.Combat;
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

        private CombatSequenceController _combatSequenceController;
        private PlayerUnit _playerUnit;
        
        private CompositeDisposable _unitSubscriptions;
        private CompositeDisposable _combatSubscriptions;
        
        [Inject]
        private void Construct(CombatSequenceController combatSequenceController, PlayerUnit playerUnit)
        {
            _combatSequenceController = combatSequenceController;
            _playerUnit = playerUnit;
        }
        
        private void Awake()
        {
            _combatSubscriptions = new();
            _combatSubscriptions.Add(_combatSequenceController.OnCombatStarted.Subscribe((enemy) =>
            {
                _unitSubscriptions = new();
                SubscribeToUnit(_playerUnit);
                SubscribeToUnit(enemy);
            }));
            
            _combatSubscriptions.Add(_combatSequenceController.OnCombatEnded.Subscribe(_ =>
            {
                _unitSubscriptions.Dispose();
            }));
        }

        private void OnDestroy()
        {
            _combatSubscriptions.Dispose();
        }

        private void SubscribeToUnit(GameUnit gameUnit)
        {
            string unitName = gameUnit.EntityName;
            var buffsData = gameUnit.UnitBuffsData;

            _unitSubscriptions.Add(
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
                }));
        }

        private void DisplayBuff(string unitName, string buffText)
        {
            _textDisplay.ShowText($"{unitName} {buffText}!");
        }
    }
}