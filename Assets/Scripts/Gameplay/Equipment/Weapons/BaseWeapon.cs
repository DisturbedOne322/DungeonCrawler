using Gameplay.Combat;
using Gameplay.Combat.Modifiers.Instances.Defensive;
using Gameplay.Combat.Modifiers.Instances.Offensive;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay.Equipment.Weapons
{
    public abstract class BaseWeapon : BaseGameItem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField, Space] private OffensiveModifierSo _offensiveModifier;
        [SerializeField] private DefensiveModifierSo _defensiveModifier;

        public GameObject Prefab => _prefab;
        public OffensiveModifierSo OffensiveModifier => _offensiveModifier;
        public DefensiveModifierSo DefensiveModifier => _defensiveModifier;
    }
}