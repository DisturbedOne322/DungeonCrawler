using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Rooms
{
    public abstract class StopRoom : DungeonRoom
    {
        public virtual UniTask PlayEnterSequence() => UniTask.CompletedTask;
        public virtual UniTask ClearRoom() => UniTask.CompletedTask;
    }
}