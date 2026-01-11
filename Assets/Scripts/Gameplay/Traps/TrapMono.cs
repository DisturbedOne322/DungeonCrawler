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
        
        public UnitHeldStatusEffectsContainer UnitHeldStatusEffectsContainer { get; } = new ();
        public UnitActiveStatusEffectsContainer UnitActiveStatusEffectsContainer { get; } = new();
        public UnitStatsData UnitStatsData { get; } = new();
        public UnitBonusStatsData UnitBonusStatsData { get; } = new();

        [Inject]
        private void Construct(HitProcessor hitProcessor, StatusEffectsProcessor statusEffectsProcessor)
        {
            _hitProcessor =  hitProcessor;
            _statusEffectsProcessor = statusEffectsProcessor;
        }

        private void Start()
        {
            UnitStatsData.SetStats(new());
            UnitBonusStatsData.SetData(new());
        }

        public void TriggerTrap()
        {
            bool hasSkills = HasValidSkills();
            bool hasDebuffs = HasValidDebuffs();

            if (!hasSkills && !hasDebuffs) return;
            if (!hasSkills) { ApplyDebuff(); return; }
            if (!hasDebuffs) { DealDamage(); return; }

            if (Random.value < _trapData.DamageChance)
                DealDamage();
            else
                ApplyDebuff();
        }
        
        private void DealDamage()
        {
            var skills = _trapData.Skills;
            int index = Random.Range(0, skills.Count);
            
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
            int index = Random.Range(0, selection.Count);
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