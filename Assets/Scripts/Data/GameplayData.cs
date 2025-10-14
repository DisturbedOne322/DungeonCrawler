using UniRx;

namespace Data
{
    public class GameplayData
    {
        public ReactiveProperty<int> PlayerPositionIndex = new();
    }
}