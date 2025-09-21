using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Areas
{
    public abstract class StopRoom : DungeonRoom
    {
        public virtual UniTask PlayEnterSequence() => UniTask.CompletedTask;
        public virtual UniTask ClearRoom() => UniTask.CompletedTask;
    }
}