using System;
using Gameplay.Equipment;
using Gameplay.Units;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class EquipmentStatBuffsApplier : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly PlayerUnit _unit;

        public EquipmentStatBuffsApplier(PlayerUnit unit,
            UnitEquipmentData unitEquipmentData)
        {
            _unit = unit;

            _compositeDisposable.Add(unitEquipmentData.OnWeaponEquipped.Subscribe(AddStatIncreasesFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnArmorEquipped.Subscribe(AddStatIncreasesFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveStatIncreasesFromEquipment));
            _compositeDisposable.Add(unitEquipmentData.OnArmorRemoved.Subscribe(RemoveStatIncreasesFromEquipment));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void AddStatIncreasesFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            var statBuffs = equipmentPiece.StatsIncreaseData;
            ApplyStats(statBuffs, +1);
        }

        private void RemoveStatIncreasesFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            var statBuffs = equipmentPiece.StatsIncreaseData;
            ApplyStats(statBuffs, -1);
        }

        private void ApplyStats(EquipmentStatsIncreaseData data, int multiplier)
        {
            var unitStatsData = _unit.UnitStatsData;

            unitStatsData.ConstitutionProp.Value += data.Constitution * multiplier;
            unitStatsData.DexterityProp.Value += data.Dexterity * multiplier;
            unitStatsData.IntelligenceProp.Value += data.Intelligence * multiplier;
            unitStatsData.LuckProp.Value += data.Luck * multiplier;
            unitStatsData.StrengthProp.Value += data.Strength * multiplier;
        }
    }
}