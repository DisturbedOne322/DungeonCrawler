using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Areas
{
    public abstract class StopRoom : DungeonRoom
    {
        public abstract UniTask SetupRoom();
        public abstract UniTask ClearRoom();
    }
}