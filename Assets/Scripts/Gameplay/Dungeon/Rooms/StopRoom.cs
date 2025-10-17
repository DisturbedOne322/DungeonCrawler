using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Rooms
{
    public abstract class StopRoom : DungeonRoom
    {
        public virtual UniTask PlayEnterSequence()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask ClearRoom()
        {
            return UniTask.CompletedTask;
        }
    }
}