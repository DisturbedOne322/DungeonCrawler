using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBuffsData
    {
        public ReactiveProperty<bool> Guarded = new(false);
        public ReactiveProperty<bool> Charged = new(false);
        public ReactiveProperty<bool> Concentrated = new(false);
        
        
    }
}