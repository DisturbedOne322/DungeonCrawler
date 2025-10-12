namespace Gameplay.Combat.Data
{
    public class HitData
    {
        public readonly int HitIndex;
        
        //result stats
        public DamageType DamageType;
        public bool IsCritical;
        public bool Missed;
        
        //stats that get buffed
        public int Damage;
        public float HitChance;
        public float CritChance;

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