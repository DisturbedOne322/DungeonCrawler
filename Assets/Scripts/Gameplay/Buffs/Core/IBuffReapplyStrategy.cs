namespace Gameplay.Buffs.Core
{
    public interface IBuffReapplyStrategy
    {
        public void ReapplyBuff(BaseBuffInstance instance, BaseBuffData data);
    }
}