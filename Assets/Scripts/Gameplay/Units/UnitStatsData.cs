using UniRx;

namespace Gameplay.Units
{
    public class UnitStatsData
    {
        public ReactiveProperty<int> ConstitutionProp;
        public ReactiveProperty<int> DexterityProp;
        public ReactiveProperty<int> IntelligenceProp;
        public ReactiveProperty<int> LuckProp;
        public ReactiveProperty<int> StrengthProp;
        
        public void SetStats(UnitStartingStats stats)
        {
            ConstitutionProp = new ReactiveProperty<int>(stats.Constitution);
            StrengthProp = new ReactiveProperty<int>(stats.Strength);
            DexterityProp = new ReactiveProperty<int>(stats.Dexterity);
            IntelligenceProp = new ReactiveProperty<int>(stats.Intelligence);
            LuckProp = new ReactiveProperty<int>(stats.Luck);
        }
    }
}