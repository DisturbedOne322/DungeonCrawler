using Gameplay.Combat.AI;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class BaseDefensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected DefensiveBuffData DefensiveBuffData;

        public override float EvaluateAction(AIActionEvaluationService evaluationService, AIContext context)
        {
            return evaluationService.GetDefensiveBuffValue(context, DefensiveBuffData);
        }
    }
}