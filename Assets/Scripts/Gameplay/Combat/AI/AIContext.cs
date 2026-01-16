using Gameplay.Facades;

namespace Gameplay.Combat.AI
{
    public struct AIContext
    {
        public IGameUnit ActiveUnit;
        public IGameUnit OtherUnit;
        public AIConfig Config;
    }
}