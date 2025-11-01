using UniRx;

namespace Gameplay.Units
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
            Constitution = new (stats.Constitution);
            Strength = new (stats.Strength);
            Dexterity = new (stats.Dexterity);
            Intelligence = new (stats.Intelligence);
            Luck = new (stats.Luck);
        }
    }
}