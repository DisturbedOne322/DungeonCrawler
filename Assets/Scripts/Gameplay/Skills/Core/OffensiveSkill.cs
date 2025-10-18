using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
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

            var hitsStream = combatService.CreateHitsStream(GetSkillData(combatService.ActiveUnit));
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

        private UniTask StartAnimation(CombatService combatService)
        {
            if (combatService.IsPlayerTurn())
                return combatService.ActiveUnit.AttackAnimator.PlayAnimation(_skillAnimationData.FpvAnimationClip);

            return combatService.ActiveUnit.AttackAnimator.PlayAnimation(_skillAnimationData.TpvAnimationClip);
        }

        protected virtual SkillData GetSkillData(IEntity entity)
        {
            return _skillData;
        }
    }
}