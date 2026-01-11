using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class StatusEffectsProcessor
    {
        private readonly PlayerUnit _playerUnit;

        public StatusEffectsProcessor(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }
        
        public void EnableStatusEffectsOnTrigger(ICombatant activeUnit, ICombatant otherUnit,
            StatusEffectTriggerType triggerType)
        {
            var heldStatusEffects = activeUnit.UnitHeldStatusEffectsContainer.All;

            for (var i = heldStatusEffects.Count - 1; i >= 0; i--)
            {
                var heldData = heldStatusEffects[i];
                var effect = heldData.Effect;
                
                if (effect.TriggerType != triggerType)
                    continue;

                var source = heldData.Source;
                
                CreateStatusEffect(activeUnit, otherUnit, effect, source);
            }
        }

        public void ApplyStatusEffectToPlayer(BaseStatusEffectData statusEffect, BaseGameItem source) => 
            ApplyStatusEffectTo(_playerUnit, statusEffect, source);

        public void ApplyStatusEffectTo(ICombatant unit, BaseStatusEffectData statusEffect, BaseGameItem source) => 
            CreateStatusEffect(unit, null, statusEffect, source);

        public void AddStatusEffectTo(IGameUnit unit, BaseStatusEffectData statusEffect, BaseGameItem source)
        {
            unit.UnitHeldStatusEffectsContainer.Add(new (statusEffect, source));
            
            if(!StatusEffectsHelper.IsTriggeredOnObtain(statusEffect))
                return;
            
            ApplyStatusEffectTo(unit, statusEffect, source);
        }
        
        public void ProcessTurn(ICombatant activeUnit)
        {
            var activeStatusEffects =
                activeUnit.UnitActiveStatusEffectsContainer.All;

            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var instance = activeStatusEffects[i];

                if (!StatusEffectsHelper.IsTurnBased(instance))
                    continue;

                if (instance.DurationLeft.Value == 0)
                    instance.Revert();

                instance.DurationLeft.Value--;
            }
        }
        
        public void ProcessDepthIncrease()
        {
            var activeStatusEffects =
                _playerUnit.UnitActiveStatusEffectsContainer.All;

            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var instance = activeStatusEffects[i];

                if (!StatusEffectsHelper.IsDepthBased(instance))
                    continue;

                if (instance.DurationLeft.Value == 0)
                    instance.Revert();

                instance.DurationLeft.Value--;
            }
        }

        public void RemoveStatusEffectsOnAction(IGameUnit activeUnit, StatusEffectExpirationType effectExpirationType)
        {
            if (!StatusEffectsHelper.IsExpirationTypeActionBased(effectExpirationType))
                return;

            var activeStatusEffects = activeUnit.UnitActiveStatusEffectsContainer.All;

            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = activeStatusEffects[i];
                if (statusEffect.EffectExpirationType != effectExpirationType)
                    continue;

                statusEffect.Revert();
            }
        }

        public void RemoveStatusEffectFromSource(IGameUnit unit, BaseGameItem source)
        {
            var heldStatusEffects = unit.UnitHeldStatusEffectsContainer;
            heldStatusEffects.RemoveFromSource(source);
            
            var activeStatusEffects = unit.UnitActiveStatusEffectsContainer.All;

            for (int i = activeStatusEffects.Count - 1; i >= 0; i--)
            {                
                var activeStatusEffect = activeStatusEffects[i];
                
                if(!StatusEffectsHelper.SameSource(activeStatusEffect, source))
                    continue;
                
                activeStatusEffect.Revert();
            }
        }

        public void ClearAllStatusEffects(IGameUnit activeUnit)
        {
            var allEffects = activeUnit.UnitActiveStatusEffectsContainer.All;

            for (var index = allEffects.Count - 1; index >= 0; index--)
            {
                var effect = allEffects[index];
                
                if (!StatusEffectsHelper.IsClearableStatusEffect(effect))
                    continue;

                effect.Revert();
            }
        }

        private void CreateStatusEffect(ICombatant activeUnit, ICombatant otherUnit,
            BaseStatusEffectData statusEffectData, BaseGameItem source)
        {
            var isIndependent = statusEffectData.StatusEffectReapplyType.HasFlag(StatusEffectReapplyType.Independent);

            var activeStatusEffects =
                activeUnit.UnitActiveStatusEffectsContainer.All;
            
            if (!isIndependent)
                for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
                {
                    var activeBuff = activeStatusEffects[i];
                    
                    if (activeBuff.StatusEffectData != statusEffectData)
                        continue;
                    
                    if (!StatusEffectsHelper.SameSource(activeBuff, source))
                        continue;
                    
                    var expirationType = activeBuff.StatusEffectData.StatusEffectDurationData.EffectExpirationType;
                    if (expirationType == StatusEffectExpirationType.Reapply)
                    {
                        activeBuff.Revert();
                        return;
                    }

                    StatusEffectsHelper.ReapplyStatusEffect(activeBuff);
                    return;
                }
            
            var instance =  statusEffectData.CreateInstance();
            instance.Apply(new (source, activeUnit, otherUnit));
        }
    }
}