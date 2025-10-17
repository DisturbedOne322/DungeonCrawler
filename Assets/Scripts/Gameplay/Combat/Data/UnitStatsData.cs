using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitStatsData
    {
        public ReactiveProperty<int> Constitution;
        public ReactiveProperty<int> Dexterity;
        public ReactiveProperty<int> Intelligence;
        public ReactiveProperty<int> Luck;
        public ReactiveProperty<int> Strength;

        public void SetStats(UnitStartingStats stats)
        {
            Constitution = new ReactiveProperty<int>(stats.Constitution);
            Strength = new ReactiveProperty<int>(stats.Strength);
            Dexterity = new ReactiveProperty<int>(stats.Dexterity);
            Intelligence = new ReactiveProperty<int>(stats.Intelligence);
            Luck = new ReactiveProperty<int>(stats.Luck);
        }
    }
}