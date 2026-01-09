using Cysharp.Threading.Tasks;

namespace Gameplay.Dungeon.Rooms
{
    public abstract class InteractiveRoom : DungeonRoom
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