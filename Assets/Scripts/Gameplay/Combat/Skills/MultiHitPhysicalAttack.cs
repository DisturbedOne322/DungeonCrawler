using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Gameplay/Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        protected override async UniTask PerformAction(CombatService combatService)
        {
            var animationTask = StartAnimation(combatService);

            int hits = SkillAnimationData.HitTimings.Count;
            
            for (int i = 0; i < hits - 1; i++) 
                await ProcessHit(combatService, i, false);

            await ProcessHit(combatService, hits - 1);
            
            await animationTask;
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}