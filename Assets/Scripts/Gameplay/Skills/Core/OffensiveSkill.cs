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
        [SerializeField, Space] private SkillAnimationData _skillAnimationData;
        [SerializeField] private SkillData _skillData;

        protected override async UniTask PerformAction(CombatService combatService)
        {
            var task = StartAnimation(combatService);

            var hitsStream = combatService.CreateFinalHitsStream(GetSkillData(combatService.ActiveUnit));
            int hitsCount = hitsStream.Hits.Count;

            float animStartTime = _skillAnimationData.SwingTiming;
            float animEndTime = _skillAnimationData.RecoveryTiming;

            float animStep = (animEndTime - animStartTime) / hitsCount;
            
            await DealHit(combatService, hitsStream, 0, animStartTime);
            
            for (int i = 1; i < hitsCount; i++) 
                await DealHit(combatService, hitsStream, i, animStep);
            
            await task;
        }

        private async UniTask DealHit(CombatService combatService, HitDataStream hitDataStream, int index, float delay)
        {
            await UniTask.WaitForSeconds(delay);
            combatService.DealDamageToOtherUnit(hitDataStream, index);
        }
        
        private UniTask StartAnimation(CombatService combatService)
        {
            if(combatService.IsPlayerTurn())
                return combatService.ActiveUnit.AttackAnimator.PlayAnimation(_skillAnimationData.FpvAnimationClip);
            
            return combatService.ActiveUnit.AttackAnimator.PlayAnimation(_skillAnimationData.TpvAnimationClip);
        }

        protected virtual SkillData GetSkillData(IEntity entity) => _skillData;

        private void OnValidate()
        {
            if(_skillData.BaseDamage < 1)
                _skillData.BaseDamage = 1;
            
            if(_skillData.MinHits < 1)
                _skillData.MinHits = 1;
            
            if(_skillData.MaxHits < _skillData.MinHits)
                _skillData.MaxHits = _skillData.MinHits;
        }

        private void Reset()
        {
            _skillData.CanBeBuffed = true;
            _skillData.CanCrit = true;
            _skillData.ConsumeStance = true;

            _skillData.BaseHitChance = 1;
        }
    }
}