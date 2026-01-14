using Gameplay.Dungeon.RoomVariants;

namespace Gameplay.Dungeon.Rooms
{
    public abstract class VariantRoom<T> : InteractiveRoom where T : RoomVariantData
    {
        protected T RoomVariantData { get; set; }
        public override RoomVariantData RoomData => RoomVariantData;

        public override void SetData(RoomVariantData data)
        {
            RoomVariantData = data as T;
        }
    }
}