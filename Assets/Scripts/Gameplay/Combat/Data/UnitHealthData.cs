using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitHealthData
    {
        private readonly ReactiveProperty<bool> _isDead = new();
        public ReactiveProperty<int> CurrentHealth = new();
        public ReactiveProperty<int> MaxHealth = new();

        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

        public void Initialize(int maxHealth)
        {
            MaxHealth.Value = maxHealth;
            CurrentHealth.Value = maxHealth;

            _isDead.Value = false;

            CurrentHealth
                .Subscribe(newHp =>
                {
                    if (newHp <= 0 && !_isDead.Value)
                        _isDead.Value = true;
                });
        }
    }
}