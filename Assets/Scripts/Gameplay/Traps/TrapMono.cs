using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Debuffs.Core;
using Gameplay.Units;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Traps
{
    public class TrapMono : MonoBehaviour, ICombatant
    {
        [SerializeField] private TrapData _trapData;

        private HitProcessor _hitProcessor;
        private StatusEffectsProcessor _statusEffectsProcessor;

        private void Start()
        {
            UnitStatsData.SetStats(new UnitStartingStats());
            UnitBonusStatsData.SetData(new UnitStartingBonusStats());
        }

        public UnitHeldStatusEffectsContainer UnitHeldStatusEffectsContainer { get; } = new();
        public UnitActiveStatusEffectsContainer UnitActiveStatusEffectsContainer { get; } = new();
        public UnitStatsData UnitStatsData { get; } = new();
        public UnitBonusStatsData UnitBonusStatsData { get; } = new();

        [Inject]
        private void Construct(HitProcessor hitProcessor, StatusEffectsProcessor statusEffectsProcessor)
        {
            _hitProcessor = hitProcessor;
            _statusEffectsProcessor = statusEffectsProcessor;
        }

        public void TriggerTrap()
        {
            var hasSkills = HasValidSkills();
            var hasDebuffs = HasValidDebuffs();

            if (!hasSkills && !hasDebuffs) return;
            if (!hasSkills)
            {
                ApplyDebuff();
                return;
            }

            if (!hasDebuffs)
            {
                DealDamage();
                return;
            }

            if (Random.value < _trapData.DamageChance)
                DealDamage();
            else
                ApplyDebuff();
        }

        private void DealDamage()
        {
            var skills = _trapData.Skills;
            var index = Random.Range(0, skills.Count);

            var skill = skills[index];
            _hitProcessor.DealDamageToPlayer(this, new HitData(skill, 0));
        }

        private void ApplyDebuff()
        {
            var debuff = SelectDebuff();
            _statusEffectsProcessor.ApplyStatusEffectToPlayer(debuff, debuff);
        }

        private StatDebuffData SelectDebuff()
        {
            var debuffs = _trapData.Debuffs;

            var selection = new List<StatDebuffData>(debuffs).Where(x => x).ToList();
            var index = Random.Range(0, selection.Count);
            return selection[index];
        }

        private bool HasValidSkills()
        {
            return _trapData.Skills.Count > 0;
        }

        private bool HasValidDebuffs()
        {
            return _trapData.Debuffs.Count(x => x) > 0;
        }
    }
}