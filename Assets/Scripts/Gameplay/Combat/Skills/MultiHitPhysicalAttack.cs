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
            var clip = SkillAnimationData.AnimationClip;
            
            var animationTask = combatService.ActiveUnit.AttackAnimator.PlayAnimation(clip);
            
            int hits = SkillAnimationData.HitTimings.Count;
            
            for (int i = 0; i < hits - 1; i++)
            {
                await UniTask.WaitForSeconds(SkillAnimationData.GetHitDelay(i));
                combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit), false);
            }

            await UniTask.WaitForSeconds(SkillAnimationData.GetHitDelay(hits - 1));
            combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit));
            
            await animationTask;
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}