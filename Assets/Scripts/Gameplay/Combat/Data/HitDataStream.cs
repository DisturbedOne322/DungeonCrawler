using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using UnityEngine;

namespace Gameplay.Combat.Data
{
    public class HitDataStream
    {
        public bool ConsumeStance;

        public DamageType DamageType;
        
        public int MaxHits;
        public int MinHits;

        public List<HitData> Hits { get; } = new();

        public static HitDataStream CreateHitsStream(SkillData skillData, IGameUnit activeUnit)
        {
            HitDataStream hitDataStream = new HitDataStream(skillData);
            
            BuffApplicationService.ApplyStructuralHitStreamBuffs(activeUnit, hitDataStream);

            var hits = Random.Range(hitDataStream.MinHits, hitDataStream.MaxHits);
            hitDataStream.CreateHitDataList(hits, skillData);

            BuffApplicationService.BuffHitStream(activeUnit, hitDataStream);
            return hitDataStream;
        }

        private HitDataStream(SkillData skillData)
        {
            MinHits = skillData.MinHits;
            MaxHits = skillData.MaxHits;

            ConsumeStance = skillData.ConsumeStance;

            DamageType = skillData.DamageType;
        }

        private void CreateHitDataList(int hits, SkillData skillData)
        {
            for (var i = 0; i < hits; i++)
                Hits.Add(new HitData(skillData, i));
        }
    }
}