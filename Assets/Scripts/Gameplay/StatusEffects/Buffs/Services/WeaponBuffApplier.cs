using System;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class WeaponBuffApplier : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly UnitHeldStatusEffectsData _unitHeldStatusEffectsData;

        public WeaponBuffApplier(UnitHeldStatusEffectsData unitHeldStatusEffectsData, UnitEquipmentData unitEquipmentData)
        {
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
                    _unitHeldStatusEffectsData.Add(statusEffect);
        }

        private void RemoveBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var statusEffect in equipmentPiece.StatusEffects)
                if (statusEffect)
                    _unitHeldStatusEffectsData.Remove(statusEffect);
        }
    }
}