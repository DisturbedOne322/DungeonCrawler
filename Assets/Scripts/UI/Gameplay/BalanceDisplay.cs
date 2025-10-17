using Gameplay.Units;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class BalanceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;

        [Inject] private PlayerUnit _playerUnit;

        private void Awake()
        {
            _playerUnit.UnitInventoryData.Coins.Subscribe(
                coins => _balanceText.text = coins.ToString()
            ).AddTo(gameObject);
        }
    }
}