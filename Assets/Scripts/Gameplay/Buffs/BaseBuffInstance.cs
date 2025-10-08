namespace Gameplay.Buffs
{
    public abstract class BaseBuffInstance
    {
        public int TurnDurationLeft;
        public BuffExpirationType ExpirationType;
        public BaseBuffData BuffData;
    }
}