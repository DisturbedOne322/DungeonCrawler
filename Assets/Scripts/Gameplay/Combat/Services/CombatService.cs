using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using Random = UnityEngine.Random;

namespace Gameplay.Combat.Services
{
    public class CombatService
    {
        private readonly BuffsCalculationService _buffsCalculationService;
        private readonly CombatData _combatData;
        private readonly CombatEventsBus _combatEventsBus;
        private readonly UnitRegenerationService _unitRegenerationService;
        private readonly HitProcessor _hitProcessor;

        public CombatService(CombatData combatData,
            CombatStatusEffectsService combatStatusEffectsService, BuffsCalculationService buffsCalculationService,
            CombatEventsBus combatEventsBus, UnitRegenerationService unitRegenerationService, HitProcessor hitProcessor)
        {
            _combatData = combatData;
            CombatStatusEffectsService = combatStatusEffectsService;
            _buffsCalculationService = buffsCalculationService;
            _combatEventsBus = combatEventsBus;
            _unitRegenerationService = unitRegenerationService;
            _hitProcessor = hitProcessor;
        }

        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;

        public int TurnCount => _combatData.TurnCount;

        public CombatStatusEffectsService CombatStatusEffectsService { get; }

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

        public HitDataStream CreateHitsStream(SkillData skillData)
        {
            HitDataStream hitDataStream = new(skillData);

            _buffsCalculationService.ApplyStructuralHitStreamBuffs(ActiveUnit, hitDataStream);

            var hits = Random.Range(hitDataStream.MinHits, hitDataStream.MaxHits);
            hitDataStream.CreateHitDataList(hits);

            _buffsCalculationService.BuffHitStream(ActiveUnit, hitDataStream);

            return hitDataStream;
        }

        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitDataStream hitDataStream, int index)
        {
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
            _hitProcessor.DealDamageToUnit(attacker, target, hitData);
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