using UnityEngine;

namespace Gameplay.Rewards
{
    [CreateAssetMenu(menuName = "Gameplay/Items/CoinsItem")]
    public class CoinsItem : BaseGameItem
    {
        [SerializeField] [Min(1)] private int _minAmount = 50;
        [SerializeField] [Min(1)] private int _maxAmount = 50;

        public int MinAmount => _minAmount;
        public int MaxAmount => _maxAmount;

        private void OnValidate()
        {
            if (_maxAmount < _minAmount)
                _maxAmount = _minAmount;
        }
    }
}