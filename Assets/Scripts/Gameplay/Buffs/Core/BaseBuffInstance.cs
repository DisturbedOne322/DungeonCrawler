namespace Gameplay.Buffs.Core
{
    public abstract class BaseBuffInstance
    {
        public int TurnDurationLeft;
        public int Stacks = 1;
        public BuffExpirationType ExpirationType;
        public BaseBuffData BuffData;
    }
}