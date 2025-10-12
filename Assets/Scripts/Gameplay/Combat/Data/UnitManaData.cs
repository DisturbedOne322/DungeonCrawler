using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitManaData
    {
        public ReactiveProperty<int> CurrentMana = new();
        public ReactiveProperty<int> MaxMana= new();
        
        public void Initialize(int maxMana)
        {
            MaxMana.Value = maxMana;
            CurrentMana.Value = maxMana;
        }
    }
}