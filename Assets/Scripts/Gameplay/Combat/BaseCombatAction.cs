using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat
{
    public abstract class BaseCombatAction : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        
        public string Name => _name;
        public string Description => _description;

        public async UniTask UseAction(CombatService combatService)
        {
            await PerformAction(combatService);
            await UniTask.WaitForSeconds(GameplayConstants.DelayAfterSkill);
        }
        
        public abstract bool CanUse(CombatService combatService);
        
        protected abstract UniTask PerformAction(CombatService combatService);
    }
}