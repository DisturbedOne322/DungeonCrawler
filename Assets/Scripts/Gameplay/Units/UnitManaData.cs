using UniRx;

namespace Gameplay.Units
{
    public class UnitManaData
    {
        public ReactiveProperty<int> CurrentMana = new();
        public ReactiveProperty<int> MaxMana = new();

        public void Initialize(int maxMana)
        {
            MaxMana.Value = maxMana;
            CurrentMana.Value = maxMana;
        }
    }
}