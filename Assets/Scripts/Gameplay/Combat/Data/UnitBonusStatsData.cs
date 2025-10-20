using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBonusStatsData
    {
        public ReactiveProperty<float> CritChanceBonus = new(0f);
        public ReactiveProperty<float> CritDamageBonus = new(0f);
        public ReactiveProperty<int> HealthRegenBonus = new(0f);

        public ReactiveProperty<int> ManaRegenBonus = new(0);

        public ReactiveProperty<float> PenetrationRatio = new(0);
    }
}