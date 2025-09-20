using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitStatsData
    {
        public ReactiveProperty<int> Constitution;
        public ReactiveProperty<int> Strength;
        public ReactiveProperty<int> Dexterity;
        public ReactiveProperty<int> Intelligence;
        public ReactiveProperty<int> Luck;

        public void SetStats(UnitBaseStats stats)
        {
            Constitution = new(stats.Constitution);
            Strength = new(stats.Strength);
            Dexterity = new(stats.Dexterity);
            Intelligence = new(stats.Intelligence);
            Luck = new(stats.Luck);
        }
    }
}