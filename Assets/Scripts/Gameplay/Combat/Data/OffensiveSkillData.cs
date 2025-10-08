namespace Gameplay.Combat.Data
{
    public struct OffensiveSkillData
    {
        public int Damage;
        public bool IsUnavoidable;
        public bool IsPiercing;
        public bool CanCrit;
        public float CritChance;
        public bool CanBeBuffed;
        public DamageType DamageType;
    }
}