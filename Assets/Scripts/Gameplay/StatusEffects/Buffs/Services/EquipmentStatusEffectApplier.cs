using System;
using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
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

            _compositeDisposable.Add(unitEquipmentData.OnWeaponEquipped.Subscribe(EquipStatusEffectsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorEquipped.Subscribe(EquipBuffsFromArmor));
            _compositeDisposable.Add(unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveBuffsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorRemoved.Subscribe(RemoveBuffsFromArmor));
        }

        public void Dispose() => _compositeDisposable.Dispose();

        private void EquipStatusEffectsFromWeapon(BaseWeapon weapon) => EquipStatusEffectsFromEquipment(weapon);

        private void EquipBuffsFromArmor(BaseArmor armor) => EquipStatusEffectsFromEquipment(armor);

        private void RemoveBuffsFromWeapon(BaseWeapon weapon) => RemoveBuffsFromEquipment(weapon);

        private void RemoveBuffsFromArmor(BaseArmor armor) => RemoveBuffsFromEquipment(armor);

        private void EquipStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                {
                    _unitHeldStatusEffectsData.Add(statusEffect);
                    TryApplyPermanentStatusEffect(statusEffect);
                }
        }

        private void RemoveBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
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
        }

        private bool IsPermanentStatusEffect(BaseStatusEffectData data)
        {
            if(data.StatusEffectDurationData.EffectExpirationType != StatusEffectExpirationType.Permanent)
                return false;
            
            if(data.TriggerType != StatusEffectTriggerType.OnObtained)
                return false;

            return true;
        }
    }
}