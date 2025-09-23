using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat
{
    public abstract class BaseCombatAction : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        
        public string Name => _name;
        public string Description => _description;

        public abstract UniTask UseAction(CombatService combatService);
        public abstract bool CanUse(CombatService combatService);
    }
}