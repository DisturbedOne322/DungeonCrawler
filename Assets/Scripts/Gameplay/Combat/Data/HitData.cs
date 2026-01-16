namespace Gameplay.Combat.Data
{
    public class HitData
    {
        public readonly int HitIndex;

        public bool CanBeBuffed;
        
        public bool CanCrit;
        public float CritChance;

        public int Damage;

        public DamageType DamageType;

        public float HitChance;

        public bool IsPiercing;
        public bool IsUnavoidable;
        
        public float PenetrationRatio;

        public bool IsCritical;
        public bool Missed;
        
        public HitData(SkillData skillData, int index)
        {
            HitIndex = index;

            Damage = skillData.BaseDamage;

            CritChance = skillData.BaseCritChance;
            CanCrit = skillData.CanCrit;

            HitChance = skillData.BaseHitChance;
            IsUnavoidable = skillData.IsUnavoidable;

            PenetrationRatio = skillData.PenetrationRatio;
            IsPiercing = skillData.IsPiercing;

            DamageType = skillData.DamageType;

            CanBeBuffed = skillData.CanBeBuffed;
        }
    }
}