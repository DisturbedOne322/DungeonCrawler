using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Units/UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameUnit _prefab;
        [SerializeField] private UnitBaseStats _unitBaseStats;
        [SerializeField] private int _maxHp;
        [SerializeField] private List<BaseSkill> _skillSet;
        
        public string Name => _name;
        public GameUnit Prefab => _prefab;
        public UnitBaseStats UnitBaseStats => _unitBaseStats;
        public int MaxHp => _maxHp;
        public List<BaseSkill> SkillSet => _skillSet;
    }
}