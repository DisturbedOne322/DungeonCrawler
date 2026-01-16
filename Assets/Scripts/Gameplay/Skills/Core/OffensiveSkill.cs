using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.AI;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class OffensiveSkill : BaseSkill
    {
        [SerializeField] [Space] private SkillAnimationData _skillAnimationData;
        [SerializeField] private SkillData _skillData;
        [SerializeReference] private List<ISkillCost> _costs = new();

        protected override async UniTask PerformAction(CombatService combatService)
        {
            foreach (var skillCost in _costs)
                skillCost.Pay(combatService);

            var task = StartAnimation(combatService);

            var activeUnit = combatService.ActiveUnit;
            var hitsStream = HitDataStream.CreateHitsStream(GetSkillData(activeUnit), activeUnit);
            var hitsCount = hitsStream.Hits.Count;

            var animStartTime = _skillAnimationData.SwingTiming;
            var animEndTime = _skillAnimationData.RecoveryTiming;

            var animStep = (animEndTime - animStartTime) / hitsCount;

            await DealHit(combatService, hitsStream, 0, animStartTime);

            for (var i = 1; i < hitsCount; i++)
                await DealHit(combatService, hitsStream, i, animStep);

            await task;
        }

        private async UniTask DealHit(CombatService combatService, HitDataStream hitDataStream, int index, float delay)
        {
            await UniTask.WaitForSeconds(delay);
            combatService.DealDamageToOtherUnit(hitDataStream, index);
        }

        public override bool CanUse(CombatService combatService)
        {
            foreach (var skillCost in _costs)
                if (!skillCost.CanPay(combatService))
                    return false;

            return true;
        }

        public override float EvaluateAction(AIActionEvaluationService evaluationService, AIContext context)
        {
            var activeUnit = context.ActiveUnit;
            return evaluationService.GetOffensiveValue(context, 
                HitDataStream.CreateHitsStream(GetSkillData(activeUnit), activeUnit));
        }

        public virtual SkillData GetSkillData(IGameUnit entity)
        {
            return _skillData;
        }

        private UniTask StartAnimation(CombatService combatService)
        {
            var activeUnit = combatService.ActiveUnit;
            
            if (combatService.IsPlayerTurn())
                return activeUnit.AttackAnimator.PlayAnimation(_skillAnimationData.FpvAnimationClip);

            return activeUnit.AttackAnimator.PlayAnimation(_skillAnimationData.TpvAnimationClip);
        }
    }
}