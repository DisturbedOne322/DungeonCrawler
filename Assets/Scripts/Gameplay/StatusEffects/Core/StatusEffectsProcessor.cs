using Gameplay.Facades;
using Helpers;

namespace Gameplay.StatusEffects.Core
{
    public class StatusEffectsProcessor
    {
        public void EnableStatusEffectsOnTrigger(IEntity activeUnit, IEntity otherUnit, StatusEffectTriggerType triggerType)
        {
            var heldStatusEffects = activeUnit.UnitHeldStatusEffectsData.All;
            
            for (var i = heldStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = heldStatusEffects[i];
                if (statusEffect.TriggerType != triggerType)
                    continue;
                
                AddStatusEffect(activeUnit, otherUnit, statusEffect);
            }
        }
        
        public void ProcessTurn(IEntity activeUnit)
        {
            var activeStatusEffects = 
                activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;
            
            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var instance = activeStatusEffects[i];
                
                if (instance.EffectExpirationType != StatusEffectExpirationType.TurnCount)
                    continue;
                
                if (instance.TurnDurationLeft.Value == 0) 
                    instance.Revert();
                
                instance.TurnDurationLeft.Value--;
            }
        }

        public void RemoveStatusEffectsOnAction(IEntity activeUnit, StatusEffectExpirationType effectExpirationType)
        {
            if (!StatusEffectsHelper.IsExpirationTypeActionBased(effectExpirationType))
                return;

            var activeStatusEffects = activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;
            
            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = activeStatusEffects[i];
                if (statusEffect.EffectExpirationType != effectExpirationType)
                    continue;

                statusEffect.Revert();
            }
        }
        
        public void ClearAllStatusEffects(IEntity activeUnit)
        {
            var allEffects = activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;

            for (var index = allEffects.Count - 1; index >= 0; index--)
            {
                var effect = allEffects[index];
                if (effect.EffectExpirationType == StatusEffectExpirationType.Permanent)
                    continue;

                effect.Revert();
            }
        }
        
        private void AddStatusEffect(IEntity activeUnit, IEntity otherUnit, 
            BaseStatusEffectData statusEffectData)
        {
            bool isIndependent = statusEffectData.StatusEffectReapplyType.HasFlag(StatusEffectReapplyType.Independent);

            var activeStatusEffects = 
                activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;
            
            if (!isIndependent)
                for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
                {
                    var activeBuff = activeStatusEffects[i];
                    if (activeBuff.StatusEffectData == statusEffectData)
                    {
                        StatusEffectsHelper.ReapplyStatusEffect(activeBuff, statusEffectData);
                        return;
                    }
                }

            statusEffectData.CreateInstance().Apply(activeUnit, otherUnit);
        }
    }
}