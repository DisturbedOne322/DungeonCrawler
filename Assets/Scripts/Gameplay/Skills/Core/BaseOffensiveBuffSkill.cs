using Gameplay.Combat.AI;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class BaseOffensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected HitBuffData HitBuffData;

        public override float EvaluateAction(AIActionEvaluationService evaluationService, AIContext context)
        {
            return evaluationService.GetOffensiveBuffValue(context, HitBuffData);
        }
    }
}