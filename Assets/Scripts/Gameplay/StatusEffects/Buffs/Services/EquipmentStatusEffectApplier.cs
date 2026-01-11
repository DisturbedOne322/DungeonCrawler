using System;
using Gameplay.Equipment;
using Gameplay.Units;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class EquipmentStatusEffectApplier : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly StatusEffectsProcessor _statusEffectProcessor;
        private readonly GameUnit _unit;

        public EquipmentStatusEffectApplier(GameUnit unit,
            StatusEffectsProcessor statusEffectProcessor)
        {
            _unit = unit;
            _statusEffectProcessor = statusEffectProcessor;

            var unitEquipmentData = unit.UnitEquipmentData;

            unitEquipmentData.OnWeaponEquipped.Subscribe(EquipStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            unitEquipmentData.OnArmorEquipped.Subscribe(EquipStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            
            unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveStatusEffectsFromEquipment).AddTo(_compositeDisposable);
            unitEquipmentData.OnArmorRemoved.Subscribe(RemoveStatusEffectsFromEquipment).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void EquipStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
            {
                if (!statusEffect)
                    continue;
                
                _statusEffectProcessor.AddStatusEffectTo(_unit, statusEffect, equipmentPiece);
            }
        }

        private void RemoveStatusEffectsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
            {
                if (!statusEffect)
                    continue;
                
                _statusEffectProcessor.RemoveStatusEffectFromSource(_unit, equipmentPiece);
            }
        }
    }
}