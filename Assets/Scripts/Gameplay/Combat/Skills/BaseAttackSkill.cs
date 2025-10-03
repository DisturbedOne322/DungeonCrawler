using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "BaseAttackSkill", menuName = "Gameplay/Skills/BaseAttackSkill")]
    public class BaseAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1;
        
        protected override async UniTask PerformAction(CombatService combatService)
        {
            var task = StartAnimation(combatService);
            
            await ProcessHit(combatService);
            
            await task;
        }

        public override bool CanUse(CombatService combatService) => true;

        protected override OffensiveSkillData GetSkillData(IEntity entity)
        {
            var skillData = base.GetSkillData(entity);
            
            int strength = entity.UnitStatsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;

            skillData.Damage = finalAttackPower;
            return skillData;
        }
    }
}