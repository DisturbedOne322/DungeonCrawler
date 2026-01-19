using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class BaseSkill : BaseCombatAction
    {
        [SerializeField] private int _cooldown = 0;
        
        public int Cooldown => _cooldown;
    }
}