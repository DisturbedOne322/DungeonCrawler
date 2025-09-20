using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class BaseSkill : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        
        public string Name => _name;
        public string Description => _description;

        public abstract UniTask UseSkill(CombatData combatData);
        public abstract bool CanUse(CombatData combatData);
    }
}