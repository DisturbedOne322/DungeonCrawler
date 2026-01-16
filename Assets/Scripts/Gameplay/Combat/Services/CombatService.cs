using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;

namespace Gameplay.Combat.Services
{
    public class CombatService
    {
        private readonly CombatData _combatData;
        private readonly CombatEventsBus _combatEventsBus;
        private readonly DamageDealingService _damageDealingService;
        private readonly StatusEffectsProcessor _statusEffectsProcessor;
        private readonly UnitRegenerationService _unitRegenerationService;

        public CombatService(CombatData combatData,
            CombatEventsBus combatEventsBus, UnitRegenerationService unitRegenerationService,
            DamageDealingService damageDealingService, StatusEffectsProcessor statusEffectsProcessor)
        {
            _combatData = combatData;
            _combatEventsBus = combatEventsBus;
            _unitRegenerationService = unitRegenerationService;
            _damageDealingService = damageDealingService;
            _statusEffectsProcessor = statusEffectsProcessor;
        }

        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;

        public int TurnCount => _combatData.TurnCount;

        public bool IsPlayerTurn()
        {
            return ActiveUnit is PlayerUnit;
        }

        public void StartCombat(EnemyUnit enemy)
        {
            _combatData.ResetCombat(enemy);
            _combatEventsBus.InvokeCombatStarted(enemy);
        }

        public void EndCombat()
        {
            _combatEventsBus.InvokeCombatEnded(_combatData.Enemy);
        }

        public void StartTurn()
        {
            _combatData.UpdateCurrentTurnUnit();
            _unitRegenerationService.RegenerateUnitInBattle(ActiveUnit);

            _combatEventsBus.InvokeTurnStarted(new TurnData
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit,
                OtherUnit = OtherUnit
            });
        }

        public void EndTurn()
        {
            _combatEventsBus.InvokeTurnEnded(new TurnData
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit
            });
        }

        public void HealActiveUnit(int amount)
        {
            HealUnit(ActiveUnit, amount);
        }

        public void DealDamageToActiveUnit(HitDataStream hitDataStream, int index)
        {
            DealDamageToUnit(ActiveUnit, ActiveUnit, hitDataStream, index);
        }

        public void DealDamageToActiveUnit(HitData hitData)
        {
            DealDamageToUnit(ActiveUnit, ActiveUnit, hitData);
        }

        public void DealDamageToOtherUnit(HitDataStream hitDataStream, int index)
        {
            DealDamageToUnit(ActiveUnit, OtherUnit, hitDataStream, index);
        }

        public void DealDamageToOtherUnit(HitData hitData)
        {
            DealDamageToUnit(ActiveUnit, OtherUnit, hitData);
        }

        public void ApplyStatusEffectToActiveUnit(BaseStatusEffectData statusEffect)
        {
            ApplyStatusEffectTo(ActiveUnit, statusEffect);
        }

        public void ApplyStatusEffectToOtherUnit(BaseStatusEffectData statusEffect)
        {
            ApplyStatusEffectTo(OtherUnit, statusEffect);
        }

        private void ApplyStatusEffectTo(ICombatant unit, BaseStatusEffectData statusEffect)
        {
            _statusEffectsProcessor.ApplyStatusEffectTo(unit, statusEffect, statusEffect);
        }

        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitDataStream hitDataStream, int index)
        {
            if (index == 0)
                _combatEventsBus.InvokeSkillCasted(new SkillCastedData
                {
                    Attacker = attacker,
                    Target = target,
                    HitDataStream = hitDataStream
                });

            var hitData = hitDataStream.Hits[index];

            DealDamageToUnit(attacker, target, hitData);

            if (index == hitDataStream.Hits.Count - 1)
                _combatEventsBus.InvokeSkillUsed(new SkillUsedData
                {
                    Attacker = attacker,
                    Target = target,
                    HitDataStream = hitDataStream
                });
        }

        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitData hitData)
        {
            _damageDealingService.DealDamageToUnit(attacker, target, hitData);
        }

        private void HealUnit(IGameUnit target, int amount)
        {
            target.UnitHealthController.AddHealth(amount);

            _combatEventsBus.InvokeHealed(new HealEventData
            {
                Target = target,
                Amount = amount
            });
        }
    }
}