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

        public abstract UniTask UseSkill(CombatService combatService);
        public abstract bool CanUse(CombatService combatService);
    }
}