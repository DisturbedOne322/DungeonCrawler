using Gameplay.Combat.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UnitManaDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _manaText;

        [Inject] private UnitManaData _manaData;

        private readonly CompositeDisposable _disposables = new ();
        
        private void Awake()
        {
            _manaData.CurrentMana.Subscribe(_ => UpdateManaDisplay()).AddTo(_disposables);
            _manaData.MaxMana.Subscribe(_ => UpdateManaDisplay()).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void UpdateManaDisplay()
        {
            var mana = _manaData.CurrentMana.Value;
            var maxMana = _manaData.MaxMana.Value;

            _manaText.text = mana + "/" + maxMana;

            if (maxMana == 0)
            {
                _slider.value = 0;
                return;
            }
            
            var fillPercent = mana * 1f / maxMana;
            _slider.value = fillPercent;
        }
    }
}