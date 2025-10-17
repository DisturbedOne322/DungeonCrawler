using System.Collections.Generic;

namespace Gameplay.Combat.Data
{
    public class HitDataStream
    {
        public int MaxHits;

        public int MinHits;

        public HitDataStream(SkillData skillData)
        {
            BaseSkillData = skillData;

            MinHits = skillData.MinHits;
            MaxHits = skillData.MaxHits;
        }

        public SkillData BaseSkillData { get; }

        public List<HitData> Hits { get; } = new();

        public void CreateHitDataList(int hits)
        {
            for (var i = 0; i < hits; i++)
                Hits.Add(new HitData(BaseSkillData, i));
        }
    }
}