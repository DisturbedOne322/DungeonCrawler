using UniRx;

namespace Data
{
    public class GameplayData
    {
        public ReactiveProperty<int> PlayerPositionIndex = new();
        public ReactiveProperty<int> Coins = new();
    }
}