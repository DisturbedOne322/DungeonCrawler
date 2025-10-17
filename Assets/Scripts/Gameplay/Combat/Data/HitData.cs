namespace Gameplay.Combat.Data
{
    public class HitData
    {
        public readonly int HitIndex;
        public float CritChance;

        //stats that get buffed
        public int Damage;

        //result stats
        public DamageType DamageType;
        public float HitChance;
        public bool IsCritical;
        public bool Missed;

        public HitData(SkillData skillData, int index)
        {
            HitIndex = index;

            Damage = skillData.BaseDamage;
            HitChance = skillData.BaseHitChance;
            CritChance = skillData.BaseCritChance;

            DamageType = skillData.DamageType;
        }
    }
}