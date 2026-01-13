using System.Collections.Generic;
using Attributes;
using Data.Constants;
using Gameplay.Consumables;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayUnits + "UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] [Space] private GameUnit _prefab;

        [SerializeField] [Space] private int _maxHp;
        [SerializeField] private int _maxMp;
        [SerializeField] private UnitStartingStats _unitStartingStats;
        [SerializeField] private UnitStartingBonusStats _unitStartingBonusStats;

        [SerializeField] [Space] private BaseSkill _basicAttackSkill;
        [SerializeField] private BaseSkill _guardSkill;
        [SerializeField] private List<BaseSkill> _skillSet;
        [SerializeField] private List<BaseConsumable> _items;
        [SerializeField] private List<BaseStatusEffectData> _statusEffects;

        [SerializeField] [Space] private BaseWeapon _baseWeapon;
        [SerializeField] private BaseArmor _baseArmor;

        public string Name => _name;
        public GameUnit Prefab => _prefab;

        public int MaxHp => _maxHp;
        public int MaxMp => _maxMp;
        public UnitStartingStats UnitStartingStats => _unitStartingStats;
        public UnitStartingBonusStats UnitStartingBonusStats => _unitStartingBonusStats;

        public BaseSkill BasicAttackSkill => _basicAttackSkill;
        public BaseSkill GuardSkill => _guardSkill;

        public List<BaseSkill> SkillSet => _skillSet;
        public List<BaseConsumable> Items => _items;
        public List<BaseStatusEffectData> StatusEffects => _statusEffects;

        public BaseWeapon BaseWeapon => _baseWeapon;
        public BaseArmor BaseArmor => _baseArmor;
    }
}