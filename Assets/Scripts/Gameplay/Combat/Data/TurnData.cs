using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public struct TurnData
    {
        public int TurnCount;
        public IGameUnit ActiveUnit;
        public IGameUnit OtherUnit;
    }
}