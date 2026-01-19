using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.AI;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using Gameplay.Units;
using Helpers;
using UnityEngine;

namespace Gameplay.Combat.SkillSelection
{
    public class AIActionSelectionProvider : BaseActionSelectionProvider
    {
        private readonly AIActionEvaluationService _actionEvaluationService;
        private readonly CombatService _combatService;
        private readonly EnemyUnit _unit;
        
        private Dictionary<BaseCombatAction, float> _actionsSelection = new();
        
        public AIActionSelectionProvider(AIActionEvaluationService actionEvaluationService,
            IGameUnit unit, CombatService combatService) :
            base(unit)
        {
            _actionEvaluationService = actionEvaluationService;
            _unit = unit as EnemyUnit;
            _combatService = combatService;
        }

        public override async UniTask<BaseCombatAction> SelectAction()
        {
            _actionsSelection.Clear();

            AIContext context = new AIContext()
            {
                ActiveUnit = _combatService.ActiveUnit,
                OtherUnit = _combatService.OtherUnit,
                Config = _unit.AIConfig
            };

            PopulateSelection(context);
            
            if (_actionsSelection.Count == 0)
                return null;

            DebugActionsSelection();

            var action = SelectAction(_actionsSelection, _unit.AIConfig.Randomness);
            
            DebugSelectedAction(action);
            
            return action;
        }

        private void PopulateSelection(AIContext context)
        {
            AddSkillActions(context);
            AddItemsActions(context);
        }

        private void AddSkillActions(AIContext context)
        {
            var skillsContainer = Unit.UnitSkillsContainer;
            var heldSkills = skillsContainer.HeldSkills;

            foreach (var skill in heldSkills) 
                TryAddAction(skill, context);
            
            TryAddAction(skillsContainer.BasicAttackSkill, context);
            TryAddAction(skillsContainer.GuardSkill, context);
        }

        private void AddItemsActions(AIContext context)
        {
            var inventory = Unit.UnitInventoryData;

            foreach (var consumable in inventory.Consumables) 
                TryAddAction(consumable, context);
        }

        private BaseCombatAction SelectAction(Dictionary<BaseCombatAction, float> actions, float randomness)
        {
            if (randomness >= 1f)
                return actions.Keys.ElementAt(Random.Range(0, actions.Count));
            
            if (randomness <= 0f)
                return actions.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            
            var adjustedScores = actions.ToDictionary(
                kvp => kvp.Key,
                kvp => Mathf.Pow(kvp.Value, 1f - randomness)
            );

            float total = adjustedScores.Values.Sum();
            float pick = Random.Range(0f, total);

            foreach (var kvp in adjustedScores)
            {
                if (pick <= kvp.Value)
                    return kvp.Key;

                pick -= kvp.Value;
            }

            return adjustedScores.Keys.First();
        }

        private void TryAddAction(SkillHeldData skillHeldData, AIContext context)
        {
            if(!skillHeldData.CanUse(_combatService))
                return;

            TryAddAction(skillHeldData.Skill, context);
        }
        
        private void TryAddAction(BaseCombatAction action, AIContext context)
        {
            if(!action.CanUse(_combatService))
                return;
            
            _actionsSelection.Add(action, action.EvaluateAction(_actionEvaluationService, context));
        }

        private void DebugActionsSelection()
        {
            if(!_unit.AIConfig.Debug)
                return;
            
            foreach (var kvp in _actionsSelection)
            {
                DebugHelper.Log($"{kvp.Key.Name}: {kvp.Value}");
            }
        }

        private void DebugSelectedAction(BaseCombatAction action)
        {
            if(_unit.AIConfig.Debug)
            {
                DebugHelper.Log(action.Name);
                DebugHelper.Log("-----------------------");
            }
        }
    }
}