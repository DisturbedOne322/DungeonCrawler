using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Areas
{
    public abstract class StopArea : DungeonArea
    {
        public abstract UniTask ClearRoom();
    }
}