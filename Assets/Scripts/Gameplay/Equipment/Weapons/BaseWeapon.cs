using Gameplay.Combat;
using Gameplay.Combat.Modifiers;
using Gameplay.Combat.Modifiers.Instances.Defensive;
using UnityEngine;

namespace Gameplay.Equipment.Weapons
{
    public abstract class BaseWeapon : BaseGameItem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private OffensiveModifierSo _offensiveModifier;
        [SerializeField] private DefensiveModifierSo _defensiveModifier;
        
        public GameObject Prefab => _prefab;
        public OffensiveModifierSo OffensiveModifier => _offensiveModifier;
        public DefensiveModifierSo DefensiveModifier => _defensiveModifier;
    }
}