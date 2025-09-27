using System.Collections.Generic;
using Gameplay.Combat.Consumables;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Gameplay/Units/UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, Space] private GameUnit _prefab;
        [SerializeField] private int _maxHp;
        [SerializeField] private UnitBaseStats _unitBaseStats;
        [SerializeField, Space] private BaseSkill _basicAttackSkill;
        [SerializeField] private BaseSkill _guardSkill;
        [SerializeField, Space] private List<BaseSkill> _skillSet;
        [SerializeField] private List<BaseConsumable> _items;

        [SerializeField, Space] private BaseWeapon _baseWeapon;
        [SerializeField] private BaseArmor _baseArmor;
        
        public string Name => _name;
        public GameUnit Prefab => _prefab;
        public int MaxHp => _maxHp;
        public UnitBaseStats UnitBaseStats => _unitBaseStats;
        public BaseSkill BasicAttackSkill => _basicAttackSkill;
        public BaseSkill GuardSkill => _guardSkill;
        public List<BaseSkill> SkillSet => _skillSet;
        public List<BaseConsumable> Items => _items;
        public BaseWeapon BaseWeapon => _baseWeapon;
        public BaseArmor BaseArmor => _baseArmor;
    }
}