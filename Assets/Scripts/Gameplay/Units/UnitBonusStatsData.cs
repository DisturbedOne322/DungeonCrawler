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

        public ReactiveProperty<int> Defense;

        public void SetData(UnitStartingBonusStats statsData)
        {
            CritChanceBonus = new(statsData.CriticalChance);
            CritDamageBonus = new(statsData.CriticalDamageBonus);
            HealthRegenBonus = new(statsData.HealthRegen);
            ManaRegenBonus = new(statsData.ManaRegen);
            PenetrationRatio = new(statsData.PenetrationRatio);
            Defense = new(statsData.Defense);
        }
    }
}