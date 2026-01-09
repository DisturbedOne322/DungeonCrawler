using System;
using System.Collections.Generic;
using Gameplay.Equipment;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class OnObtainStatusEffectApplier : IDisposable
    {
        private readonly Dictionary<BaseStatusEffectData, BaseStatusEffectInstance> _appliedStatusEffects = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly GameUnit _unit;
        private readonly UnitHeldStatusEffectsData _unitHeldStatusEffectsData;
        private readonly StatusEffectsProcessor _statusEffectsProcessor;

        public OnObtainStatusEffectApplier(GameUnit unit,
            UnitHeldStatusEffectsData unitHeldStatusEffectsData, StatusEffectsProcessor statusEffectsProcessor)
        {
            _unit = unit;
            _unitHeldStatusEffectsData = unitHeldStatusEffectsData;
            _statusEffectsProcessor = statusEffectsProcessor;

            var unitEquipmentData = _unit.UnitEquipmentData;

            unitEquipmentData.OnWeaponEquipped.Subscribe(EquipStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            unitEquipmentData.OnArmorEquipped.Subscribe(EquipStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            
            unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            unitEquipmentData.OnArmorRemoved.Subscribe(RemoveStatusEffectsFromEquipment).AddTo(_compositeDisposable);

            _unit.UnitHeldStatusEffectsData.All.ObserveAdd()
                .Subscribe(e => TryApplyPermanentStatusEffectOnObtain(e.Value)).
                AddTo(_compositeDisposable);
            
            _unit.UnitHeldStatusEffectsData.All.ObserveRemove()
                .Subscribe(e => TryRemovePermanentStatusEffect(e.Value)).
                AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void EquipStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                    _unitHeldStatusEffectsData.Add(statusEffect);
        }

        private void RemoveStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                    _unitHeldStatusEffectsData.Remove(statusEffect);
        }

        private void TryApplyPermanentStatusEffectOnObtain(BaseStatusEffectData data)
        {
            if(!IsOnObtainCondition(data))
                return;
            
            var instance = data.CreateInstance();
            instance.Apply(_unit, null);

            _appliedStatusEffects.Add(data, instance);
        }

        private void TryRemovePermanentStatusEffect(BaseStatusEffectData data)
        {
            if(!IsOnObtainCondition(data))
                return;
            
            var instance = _appliedStatusEffects[data];

            if (instance == null)
            {
                DebugHelper.LogWarning("Trying to remove null buff instance");
                return;
            }
            
            instance.Revert();
            _appliedStatusEffects.Remove(data);
        }

        private bool IsOnObtainCondition(BaseStatusEffectData data)
        {
            return data.TriggerType == StatusEffectTriggerType.OnObtained;
        }

        private bool IsPermanent(BaseStatusEffectData data)
        {
            return data.StatusEffectDurationData.EffectExpirationType == StatusEffectExpirationType.Permanent;
        }
    }
}