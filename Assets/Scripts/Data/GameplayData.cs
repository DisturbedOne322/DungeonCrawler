using Gameplay.Pause;
using UniRx;

namespace Data
{
    public class GameplayData
    {
        public readonly ReactiveProperty<GameState> CurrentGameState = new(GameState.Playing);
        public readonly ReactiveProperty<int> PlayerPositionIndex = new(0);
        public readonly ReactiveProperty<float> TimeScale = new(1);
    }
}