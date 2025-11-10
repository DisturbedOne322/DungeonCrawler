using UniRx;

namespace Gameplay.Units
{
    public class UnitBonusStatsData
    {
        public ReactiveProperty<float> CritChanceBonus;
        public ReactiveProperty<float> CritDamageBonus;

        public ReactiveProperty<int> HealthRegenBonus;

        public ReactiveProperty<int> ManaRegenBonus;

        public ReactiveProperty<float> PenetrationRatio;

        public void SetData(UnitStartingBonusStats statsData)
        {
            CritChanceBonus = new ReactiveProperty<float>(statsData.CriticalChance);
            CritDamageBonus = new ReactiveProperty<float>(statsData.CriticalDamageBonus);
            HealthRegenBonus = new ReactiveProperty<int>(statsData.HealthRegen);
            ManaRegenBonus = new ReactiveProperty<int>(statsData.ManaRegen);
            PenetrationRatio = new ReactiveProperty<float>(statsData.PenetrationRatio);
        }
    }
}