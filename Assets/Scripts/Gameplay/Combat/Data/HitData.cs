namespace Gameplay.Combat.Data
{
    public class HitData
    {
        public readonly int HitIndex;
        public int Damage;
        
        public DamageType DamageType;
        
        public bool IsCritical;
        public bool Evaded;

        public HitData(SkillData skillData, int index)
        {
            HitIndex = index;
            
            Damage = skillData.Damage;
            
            DamageType = skillData.DamageType;
        }
    }
}