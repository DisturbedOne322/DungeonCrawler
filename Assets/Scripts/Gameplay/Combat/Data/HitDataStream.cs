using System.Collections.Generic;

namespace Gameplay.Combat.Data
{
    public class HitDataStream
    {
        private readonly SkillData _baseSkillData;

        public int MinHits;
        public int MaxHits;

        public bool ConsumeStance;

        public SkillData BaseSkillData => _baseSkillData;
        public List<HitData> Hits { get; } = new();
        
        public HitDataStream(SkillData skillData)
        {
            _baseSkillData = skillData;
            
            MinHits = skillData.MinHits;
            MaxHits = skillData.MaxHits;
            
            ConsumeStance = skillData.ConsumeStance;
        }

        public void CreateHitDataList(int hits)
        {
            for (int i = 0; i < hits; i++) 
                Hits.Add(new(_baseSkillData, i));
        }
    }
}