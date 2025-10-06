using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class OffensiveSkill : BaseSkill
    {
        [SerializeField, Min(1)] protected int BaseDamage = 1;
        [SerializeField] protected SkillAnimationData SkillAnimationData;
        
        [SerializeField, Range(0, 1f), Space] private float _baseCritChance = 0;
        [SerializeField] private bool _canCrit = true;
        [SerializeField] private bool _isPiercing = false;
        [SerializeField] private bool _isUnavoidable = false;
        [SerializeField] private DamageType _damageType;

        protected async UniTask ProcessHit(CombatService combatService, int hitIndex = 0, bool consumeCharge = true)
        {
            await UniTask.WaitForSeconds(SkillAnimationData.GetHitDelay(hitIndex));
            combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit), consumeCharge);
        }
        
        protected UniTask StartAnimation(CombatService combatService)
        {
            if(combatService.IsPlayerTurn())
                return combatService.ActiveUnit.AttackAnimator.PlayAnimation(SkillAnimationData.FpvAnimationClip);
            
            return combatService.ActiveUnit.AttackAnimator.PlayAnimation(SkillAnimationData.TpvAnimationClip);
        }

        protected virtual OffensiveSkillData GetSkillData(IEntity entity)
        {
            return new OffensiveSkillData()
            {
                Damage = BaseDamage,
                CritChance = _baseCritChance,
                IsPiercing = _isPiercing,
                IsUnavoidable = _isUnavoidable,
                DamageType = _damageType,
                CanCrit = _canCrit,
            };
        }
    }
}