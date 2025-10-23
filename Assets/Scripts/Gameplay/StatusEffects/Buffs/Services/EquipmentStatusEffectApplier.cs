using System;
using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class EquipmentStatusEffectApplier : IDisposable
    {
        private readonly GameUnit _unit;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly UnitHeldStatusEffectsData _unitHeldStatusEffectsData;

        private readonly Dictionary<BaseStatusEffectData, BaseStatusEffectInstance> _appliedStatusEffects = new();
        
        public EquipmentStatusEffectApplier(GameUnit unit,
            UnitHeldStatusEffectsData unitHeldStatusEffectsData, 
            UnitEquipmentData unitEquipmentData)
        {
            _unit = unit;
            _unitHeldStatusEffectsData = unitHeldStatusEffectsData;

            _compositeDisposable.Add(unitEquipmentData.OnWeaponEquipped.Subscribe(EquipStatusEffectsFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnArmorEquipped.Subscribe(EquipStatusEffectsFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveStatusEffectsFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnArmorRemoved.Subscribe(RemoveStatusEffectsFromEquipment));
        }

        public void Dispose() => _compositeDisposable.Dispose();
        
        private void EquipStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                {
                    _unitHeldStatusEffectsData.Add(statusEffect);
                    TryApplyPermanentStatusEffect(statusEffect);
                }
        }

        private void RemoveStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                {
                    _unitHeldStatusEffectsData.Remove(statusEffect);
                    TryRemovePermanentStatusEffect(statusEffect);
                }
        }
        
        private void TryApplyPermanentStatusEffect(BaseStatusEffectData data)
        {
            if(!IsPermanentStatusEffect(data))
                return;

            var instance = data.CreateInstance();
            instance.Apply(_unit, null);
            
            _appliedStatusEffects.Add(data, instance);
        }
        
        private void TryRemovePermanentStatusEffect(BaseStatusEffectData data)
        {
            if(!IsPermanentStatusEffect(data))
                return;

            var instance = _appliedStatusEffects[data];
            instance.Revert();

            _appliedStatusEffects.Remove(data);
        }

        private bool IsPermanentStatusEffect(BaseStatusEffectData data)
        {
            if(data.StatusEffectDurationData.EffectExpirationType != StatusEffectExpirationType.Permanent)
                return false;
            
            return data.TriggerType == StatusEffectTriggerType.OnObtained;
        }
    }
}